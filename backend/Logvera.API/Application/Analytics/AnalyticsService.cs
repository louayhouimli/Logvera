using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Analytics;
using Logvera.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Application.Analytics
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly LogveraDbContext _db;

        public AnalyticsService(LogveraDbContext db)
        {
            _db = db;
        }

        public async Task<AnalyticsOverviewResponse> GetOverviewAsync(
            Guid userId,
            AnalyticsOverviewRequest request)
        {
            var query =
                from log in _db.LogEntries
                join api in _db.Apis on log.ApiId equals api.Id
                where api.UserId == userId
                      && log.ApiId == request.ApiId
                select log;

            if (request.From.HasValue)
                query = query.Where(l => l.Timestamp >= request.From);

            if (request.To.HasValue)
                query = query.Where(l => l.Timestamp <= request.To);

            var totalRequests = await query.CountAsync();

            var errorCount = await query
                .CountAsync(l => l.StatusCode >= 400);

            var avgDuration = totalRequests == 0
                ? 0
                : await query.AverageAsync(l => l.DurationMs);

            var minutes = Math.Max(
                1,
                (int)((request.To ?? DateTime.UtcNow)
                    - (request.From ?? DateTime.UtcNow.AddMinutes(-5)))
                .TotalMinutes
            );

            var rpm = totalRequests / (double)minutes;

            return new AnalyticsOverviewResponse
            {
                TotalRequests = totalRequests,
                ErrorCount = errorCount,
                ErrorRate = totalRequests == 0
                    ? 0
                    : Math.Round((double)errorCount / totalRequests * 100, 2),
                AvgDurationMs = Math.Round(avgDuration, 2),
                RequestsPerMinute = Math.Round(rpm, 2)
            };
        }
    }
}