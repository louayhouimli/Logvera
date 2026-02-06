using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Domain
{
    public class Api
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public User User { get; set; } = null!;
    }
}