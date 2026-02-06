using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Apis
{
    public class CreateApiRequest
    {
        [Required(ErrorMessage = "API name is required")]
        [MinLength(3, ErrorMessage = "API name must be at least 3 characters")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Base URL is required")]
        [Url(ErrorMessage = "Base URL must be a valid URL")]
        public string BaseUrl { get; set; } = null!;
    }
}