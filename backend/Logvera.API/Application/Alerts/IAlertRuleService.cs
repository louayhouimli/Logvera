using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Alerts;

namespace Logvera.API.Application.Alerts
{
    public interface IAlertRuleService
    {
        Task<AlertRuleResponse> CreateAsync(Guid userId, CreateAlertRuleRequest request);
        Task<List<AlertRuleResponse>> GetForUserAsync(Guid userId);
    }
}