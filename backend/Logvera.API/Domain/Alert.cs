using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Domain
{
    public class Alert
    {
        public Guid Id { get; set; }
        public Guid ApiId { get; set; }

        public string Message { get; set; } = null!;
        public bool IsRead { get; set; }

        public Guid AlertRuleId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Api Api { get; set; } = null!;
    }
}