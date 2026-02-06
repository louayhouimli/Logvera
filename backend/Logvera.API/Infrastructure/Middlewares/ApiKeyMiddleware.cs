using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Logvera.API.Infrastructure
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, LogveraDbContext db)
        {
            if (!context.Request.Path.Value!.Contains("/logs")
    || context.Request.Method != HttpMethods.Post)
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("X-API-KEY", out var apiKeyValue))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var apiKey = apiKeyValue.ToString();

            var api = await db.Apis
                .FirstOrDefaultAsync(a => a.ApiKey == apiKey);

            if (api == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            context.Items["Api"] = api;

            await _next(context);
        }
    }
}