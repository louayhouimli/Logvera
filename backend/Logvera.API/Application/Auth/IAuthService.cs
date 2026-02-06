using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Contracts;
using Logvera.API.Contracts.Auth;

namespace Logvera.API.Application.Auth
{
    public interface IAuthService
    {
        Task<UserResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}