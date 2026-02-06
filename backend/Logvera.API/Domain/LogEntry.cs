using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Domain
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public Guid ApiId { get; set; }
        public string Endpoint { get; set; } = null!;
        public string Method { get; set; } = null!;
        public int StatusCode { get; set; }
        public int DurationMs { get; set; }
        public DateTime Timestamp { get; set; }

        public Api Api { get; set; } = null!;
    }
}