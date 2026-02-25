using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Logs
{
    public class LogIngestRequest
    {
        [Required]
        public string Endpoint { get; set; } = null!;

        [Required]
        public string Method { get; set; } = null!;

        [Range(100, 599)]
        public int StatusCode { get; set; }

        [Range(0, int.MaxValue)]
        public int DurationMs { get; set; }

    }
}