using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Apis
{
    public class ApiResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
        public string ApiKey { get; set; } = null!;
    }

}