using WebApi.Middleware;

namespace WebApi.Extensions
{
    internal static class WebApplicationExtension
    {
        internal static WebApplication ConfigureWebApp(this WebApplication web)
        {
            web
                .UseWithEnrichingCors()
                .UseMiddlewares();
            return web;
        }

        private static IApplicationBuilder UseMiddlewares(this IApplicationBuilder applicationBuilder)
            => applicationBuilder
                .UseMiddleware<ApiKeyMiddleware>()
                .UseMiddleware<ExceptionMiddleware>()
                .UseSwagger()
                .UseSwaggerUI()
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });

        private static WebApplication UseWithEnrichingCors(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseCors();

            return (WebApplication)applicationBuilder;
        }
    }
}

