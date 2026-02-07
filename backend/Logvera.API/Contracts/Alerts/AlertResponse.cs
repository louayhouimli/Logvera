using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Alerts
{
    public class AlertResponse
    {
        public Guid Id { get; set; }
        public Guid ApiId { get; set; }
        public string Message { get; set; } = null!;

        public Boolean IsRead { get; set; }
        public Guid AlertRuleId { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}