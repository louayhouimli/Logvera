using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Alerts
{
    public class CreateAlertRuleRequest
    {
        [Required]
        public Guid ApiId { get; set; }

        [Range(0.1, 100)]
        public double Threshold { get; set; }

        [Range(1, 60)]
        public int WindowMinutes { get; set; }
    }
}