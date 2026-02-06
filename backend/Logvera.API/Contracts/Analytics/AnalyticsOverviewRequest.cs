using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Analytics
{
    public class AnalyticsOverviewRequest
    {
        public Guid ApiId { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}