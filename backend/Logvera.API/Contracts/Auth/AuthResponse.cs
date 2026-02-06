using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logvera.API.Contracts.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; } = null!;
        public UserResponse User { get; set; } = null!;

        public AuthResponse(string token, UserResponse user)
        {
            Token = token;
            User = user;
        }
    }
}