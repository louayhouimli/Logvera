using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts.Analytics;

namespace Logvera.API.Application.Analytics
{
    public interface IAnalyticsService
    {
        Task<AnalyticsOverviewResponse> GetOverviewAsync(
        Guid userId,
        AnalyticsOverviewRequest request
    );
    }
}