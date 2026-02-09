using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Logvera.API
{


    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {


            var statusCode = HttpStatusCode.InternalServerError;
            var title = "An unexpected error occurred.";
            var detail = "We're sorry, an error occurred. Please try again later or contact support.";


            if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                title = "Unauthorized";
                detail = exception.Message;
            }
            else if (exception is InvalidOperationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                title = "Bad Request";
                detail = exception.Message;
            }


            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path,
                // Include a TraceId for client-side correlation with server logs
                Extensions = {
                { "traceId", httpContext.TraceIdentifier }
            }
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}