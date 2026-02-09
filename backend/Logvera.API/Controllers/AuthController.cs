using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Logvera.API.Application.Auth;
using Logvera.API.Contracts;
using Logvera.API.Contracts.Auth;
using Logvera.API.Domain;
using Logvera.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace Logvera.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim?.Value) || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return Ok(null);
            }
            var userResponse = await _authService.GetUserByIdAsync(userId);
            if (userResponse == null)
            {
                return Ok(null);
            }
            return Ok(userResponse);


        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userResponse = await _authService.RegisterAsync(request);
            if (userResponse == null)
            {
                throw new InvalidOperationException("Email is already in use.");
            }
            return Ok(userResponse);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMilliseconds(10 * 60 * 1000)
            };
            HttpContext.Response.Cookies.Append("accessToken", result.Token, cookieOptions);

            return Ok(result);

        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("accessToken");
            return Ok();
        }


        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Authenticated!");
        }
    }

}