using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Logvera.API.Application.Logs;
using Logvera.API.Contracts.Logs;
using Logvera.API.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logvera.API.Controllers
{
    [ApiController]
    [Route("api/logs")]
    public class LogsController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<IActionResult> Ingest([FromBody] LogIngestRequest request)
        {
            var api = HttpContext.Items["Api"] as Api;

            if (api == null)
                return Unauthorized();

            await _logService.IngestAsync(api.Id, request);

            return Accepted();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Query([FromQuery] LogQueryRequest query)
        {
            var userId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier)!
            );

            var (logs, total) = await _logService.QueryAsync(userId, query);

            return Ok(new
            {
                total,
                page = query.Page,
                pageSize = query.PageSize,
                logs
            });
        }

    }
}