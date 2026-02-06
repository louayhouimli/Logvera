using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Logvera.API.Contracts;
using Logvera.API.Contracts.Auth;
using Logvera.API.Domain;
using Logvera.API.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Logvera.API.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly LogveraDbContext _db;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IConfiguration config, LogveraDbContext db, IPasswordHasher<User> passwordHasher)
        {
            _config = config;
            _db = db;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || _passwordHasher.VerifyHashedPassword(user!, user!.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                return null!;
            return new AuthResponse(
                GenerateJwt(user),
                new UserResponse
                {
                    Id = user.Id,
                    Email = user.Email
                }
            );
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return null!;
            }
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return new UserResponse
            {
                Id = user.Id,
                Email = user.Email
            };
        }

        private string GenerateJwt(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email)
    };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}