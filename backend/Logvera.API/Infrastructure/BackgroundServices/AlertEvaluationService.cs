using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Infrastructure
{
    public class AlertEvaluationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public AlertEvaluationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await EvaluateAlertsAsync(stoppingToken);
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task EvaluateAlertsAsync(CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LogveraDbContext>();

            var now = DateTime.UtcNow;

            var rules = await db.AlertRules
                .Where(r => r.IsActive)
                .ToListAsync(ct);

            foreach (var rule in rules)
            {
                var windowStart = now.AddMinutes(-rule.WindowMinutes);

                var totalRequests = await db.LogEntries
                    .Where(l => l.ApiId == rule.ApiId && l.Timestamp >= windowStart)
                    .CountAsync(ct);

                if (totalRequests == 0)
                    continue;

                var errorCount = await db.LogEntries
                    .Where(l =>
                        l.ApiId == rule.ApiId &&
                        l.Timestamp >= windowStart &&
                        l.StatusCode >= 500)
                    .CountAsync(ct);

                var errorRate = (double)errorCount / totalRequests * 100;

                if (errorRate <= rule.Threshold)
                    continue;

                var recentlyTriggered = await db.Alerts.AnyAsync(a =>
                    a.AlertRuleId == rule.Id &&
                    a.CreatedAt >= windowStart,
                    ct);

                if (recentlyTriggered)
                    continue;

                var alert = new Alert
                {
                    Id = Guid.NewGuid(),
                    ApiId = rule.ApiId,
                    AlertRuleId = rule.Id,
                    Message = $"5xx error rate reached {Math.Round(errorRate, 2)}%",
                    CreatedAt = now,
                    IsRead = false
                };

                db.Alerts.Add(alert);
                await db.SaveChangesAsync(ct);
            }
        }
    }
}