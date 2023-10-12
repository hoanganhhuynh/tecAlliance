using Microsoft.Extensions.Primitives;
using System.Net;
using WebApi.Models;
using System.Text.Json;

namespace WebApi.Middleware
{
    public class ApiKeyMiddleware
    {
        private const string Token = "Authorization";
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiKeyMiddleware> _logger;

        public ApiKeyMiddleware(RequestDelegate next,
            ILogger<ApiKeyMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next.Invoke(httpContext);
                return;
            }

            var bResult = httpContext.Request.Headers.TryGetValue(Token, out StringValues apiToken);
            var token = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.egWqG77-0tfW57GbM6m7snhJCKMXPcZv2Gx0x-Q6-vk";

            if (bResult && apiToken.Any(x => x == token))
            {
                await _next.Invoke(httpContext);
                return;
            }

            httpContext.Response.ContentType = "application/json";
            ErrorResult errorDetail = new ErrorResult((int)HttpStatusCode.Unauthorized, $"{Token} not found");

            httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorDetail)).ConfigureAwait(false);
        }
    }
}

