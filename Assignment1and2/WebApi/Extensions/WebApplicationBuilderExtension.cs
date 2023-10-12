using System.Reflection;
using Application.MappingProfile;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Controllers;

namespace WebApi.Extensions
{
    internal static class WebApplicationBuilderExtension
    {
        internal static WebApplicationBuilder RegisterDependencyServices(
            this WebApplicationBuilder builder,Assembly applicationAssembly)
        {
            var services = builder.Services;
            services
                .AddSwaggerGen()
                .AddAutoMapper(typeof(EmployeeProfile))
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly))
                .RegisterRepositories()
                .AddCorsPolicies()
                .AddControllers();
          
            return builder;
        }
    }
}

