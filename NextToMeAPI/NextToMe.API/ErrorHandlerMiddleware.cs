using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NextToMe.Common.Exceptions;
using System.Text.Json;
using System.Threading.Tasks;

namespace NextToMe.API
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                await WriteDevelopmentResponse(context);
            }
            else
            {
                await WriteProductionResponse(context);
            }
        }

        private Task WriteDevelopmentResponse(HttpContext httpContext)
            =>
                WriteResponse(httpContext, includeDetails: true);

        private Task WriteProductionResponse(HttpContext httpContext)
            =>
                WriteResponse(httpContext, includeDetails: false);

        private async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var ex = exceptionDetails?.Error as BaseException;

            if (ex != null)
            {
                _logger.LogError(ex, ex.LoggedMessage);
                httpContext.Response.ContentType = "application/problem+json";

                var details = includeDetails ? ex.ToString() : null;

                var problem = new
                {
                    status = (int)ex.Code,
                    message = ex.Message,
                    details = details
                };
                httpContext.Response.StatusCode = (int)ex.Code;

                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problem);
            }
            else
            {
                _logger.LogError(exceptionDetails?.Error, "Unhandled exception");
                var stream = httpContext.Response.Body;
                await JsonSerializer.SerializeAsync(stream, exceptionDetails?.Error.Message);
            }
        }
    }
    // https://andrewlock.net/creating-a-custom-error-handler-middleware-function/
    public static class CustomErrorsHandler
    {
        public static void UseCustomErrors(this IApplicationBuilder app, IHostEnvironment environment)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
