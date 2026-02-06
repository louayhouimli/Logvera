using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logvera.API.Application.Alerts;
using Logvera.API.Contracts.Alerts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logvera.API.Controllers
{
    [ApiController]
    [Route("api/alert-rules")]
    [Authorize]
    public class AlertRulesController : ControllerBase
    {
        private readonly IAlertRuleService _service;

        public AlertRulesController(IAlertRuleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAlertRuleRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _service.CreateAsync(userId, request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var result = await _service.GetForUserAsync(userId);
            return Ok(result);
        }
    }
}