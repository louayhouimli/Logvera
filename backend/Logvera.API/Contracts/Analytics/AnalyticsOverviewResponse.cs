using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Analytics
{
    public class AnalyticsOverviewResponse
    {
        public int TotalRequests { get; set; }
        public int ErrorCount { get; set; }
        public double ErrorRate { get; set; }
        public double AvgDurationMs { get; set; }
        public double RequestsPerMinute { get; set; }
    }
}