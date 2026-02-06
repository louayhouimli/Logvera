using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Domain
{
    public class AlertRule
    {
        public Guid Id { get; set; }
        public Guid ApiId { get; set; }

        public double Threshold { get; set; }
        public int WindowMinutes { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public Api Api { get; set; } = null!;
    }
}