using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Alerts;

namespace Logvera.API.Application.Alerts
{
    public interface IAlertService
    {
        Task<List<AlertResponse>> GetAlertsByApiIdAsync(Guid apiId);
        Task<bool> SetAlertAsReadAsync(Guid alertId);
    }
}