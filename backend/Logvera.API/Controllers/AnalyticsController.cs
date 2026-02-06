using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logvera.API.Application.Analytics;
using Logvera.API.Contracts.Analytics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logvera.API.Controllers
{
    [ApiController]
    [Route("api/analytics")]
    [Authorize]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("overview")]
        public async Task<IActionResult> Overview(
            [FromQuery] AnalyticsOverviewRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _analyticsService
                .GetOverviewAsync(userId, request);

            return Ok(result);
        }
    }
}