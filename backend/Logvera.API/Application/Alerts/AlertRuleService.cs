using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Alerts;
using Logvera.API.Domain;
using Logvera.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Application.Alerts
{
    public class AlertRuleService : IAlertRuleService
    {
        private readonly LogveraDbContext _db;

        public AlertRuleService(LogveraDbContext db)
        {
            _db = db;
        }

        public async Task<AlertRuleResponse> CreateAsync(Guid userId, CreateAlertRuleRequest request)
        {
            var api = await _db.Apis
                .FirstOrDefaultAsync(a => a.Id == request.ApiId && a.UserId == userId);

            if (api == null)
                throw new InvalidOperationException("API not found");

            var rule = new AlertRule
            {
                Id = Guid.NewGuid(),
                ApiId = api.Id,
                Threshold = request.Threshold,
                WindowMinutes = request.WindowMinutes,
                CreatedAt = DateTime.UtcNow
            };

            _db.AlertRules.Add(rule);
            await _db.SaveChangesAsync();

            return new AlertRuleResponse
            {
                Id = rule.Id,
                ApiId = rule.ApiId,
                Threshold = rule.Threshold,
                WindowMinutes = rule.WindowMinutes,
                IsActive = rule.IsActive
            };
        }

        public async Task<List<AlertRuleResponse>> GetForUserAsync(Guid userId)
        {
            return await _db.AlertRules
                .Join(_db.Apis, r => r.ApiId, api => api.Id, (r, api) => new { r, api })
                .Where(x => x.api.UserId == userId)
                .Select(x => new AlertRuleResponse
                {
                    Id = x.r.Id,
                    ApiId = x.r.ApiId,
                    Threshold = x.r.Threshold,
                    WindowMinutes = x.r.WindowMinutes,
                    IsActive = x.r.IsActive
                })
                .ToListAsync();
        }
    }
}