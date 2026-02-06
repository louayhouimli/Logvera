using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid.")]
        public string Email { get; set; } = null!;


        [Required(ErrorMessage = "Passowrd is required")]
        [MinLength(8, ErrorMessage = "Passowrd length must not be less than 8 characters")]
        public string Password { get; set; } = null!;
    }
}