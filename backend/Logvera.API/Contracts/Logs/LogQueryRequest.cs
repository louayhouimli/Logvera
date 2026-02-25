using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Logs
{
    public class LogQueryRequest
    {
        public Guid? ApiId { get; set; }

        public string? Endpoint { get; set; }
        public string? Method { get; set; }
        public int? StatusCode { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}