using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logvera.API.Application.Alerts;
using Logvera.API.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logvera.API.Controllers
{
    [ApiController]
    [Route("api/alerts")]
    [Authorize]
    public class AlertsController : ControllerBase
    {

        private readonly IAlertService _alertService;

        public AlertsController(IAlertService alertService)
        {
            _alertService = alertService;
        }

        [HttpGet("{apiId}")]
        public async Task<IActionResult> GetAlertsByApiId(Guid apiId)
        {
            var alerts = await _alertService.GetAlertsByApiIdAsync(apiId);
            return Ok(alerts);



        }
        [HttpPatch("{alertId}/read")]
        public async Task<IActionResult> SetAlertAsRead(Guid alertId)
        {
            var result = await _alertService.SetAlertAsReadAsync(alertId);
            if (!result)
            {
                return NotFound("Alert not found.");
            }
            return NoContent();
        }
    }
}