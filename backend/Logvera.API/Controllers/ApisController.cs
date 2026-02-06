using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Logvera.API.Application.Apis;
using Logvera.API.Contracts.Apis;
using Logvera.API.Domain;
using Logvera.API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logvera.API.Controllers;

[ApiController]
[Route("api/apis")]
[Authorize]
public class ApisController : ControllerBase
{
    private readonly IApiService _apiService;

    public ApisController(IApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateApiRequest request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );
        var result = await _apiService.CreateApiAsync(userId, request);
        return Ok(result);


    }

    [HttpGet]
    public async Task<IActionResult> GetMyApis()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!
        );

        var result = await _apiService.GetUserApisAsync(userId);
        return Ok(result);
    }


}