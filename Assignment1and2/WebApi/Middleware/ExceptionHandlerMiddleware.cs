using System.Net;
using System.Text.Json;
using WebApi.Models;

namespace WebApi.Middleware
{
    public sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError("{@Exception}", ex);
                var innerException = ex.InnerException;

                while (innerException != null)
                {
                    _logger.LogError("{@InnerException}", innerException);
                    innerException = innerException.InnerException;
                }

                var errorDetail = ResolveError(ex);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = errorDetail.ErrorCode;
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorDetail)).ConfigureAwait(false);
            }
        }

        private ErrorResult ResolveError(Exception exception)
        {
            switch (exception.GetType().Name)
            {
                case "ApplicationException":
                    return new ErrorResult((int)HttpStatusCode.Forbidden, exception.Message);
                case "NotFoundException":
                    return new ErrorResult((int)HttpStatusCode.NotFound, exception.Message);
                default:
                    return new ErrorResult((int)HttpStatusCode.InternalServerError, "An unhandled exception has occurred, please check the log for details.");
            }
        }

    }
}

