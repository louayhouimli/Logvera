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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userResponse = await _authService.RegisterAsync(request);
            if (userResponse == null)
            {
                return BadRequest("Email already exists.");
            }
            return Ok(userResponse);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
            {
                return Unauthorized("Email or passowrd is incorrect.");
            }
            return Ok(result);

        }


        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Authenticated!");
        }
    }

}