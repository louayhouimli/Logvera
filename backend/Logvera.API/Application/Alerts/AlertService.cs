using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Alerts;
using Logvera.API.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Application.Alerts
{
    public class AlertService : IAlertService
    {

        private readonly LogveraDbContext _db;
        public AlertService(LogveraDbContext db)
        {
            _db = db;
        }


        public async Task<List<AlertResponse>> GetAlertsByApiIdAsync(Guid apiId)
        {
            var alerts = await _db.Alerts.Where(a => a.ApiId == apiId).ToListAsync();
            return alerts.Select(a => new AlertResponse
            {
                Id = a.Id,
                ApiId = a.ApiId,
                Message = a.Message,
                IsRead = a.IsRead,
                AlertRuleId = a.AlertRuleId,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
        public async Task<bool> SetAlertAsReadAsync(Guid alertId)
        {
            var alert = await _db.Alerts.FindAsync(alertId);
            if (alert != null)
            {
                alert.IsRead = true;
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}