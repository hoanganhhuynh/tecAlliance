using System;
using Domain.Options;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Net.Http.Headers;

namespace WebApi.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            return services;
        }

        internal static IServiceCollection AddConfigurationOptions(this IServiceCollection serviceCollection,
       ConfigurationManager configuration)
       => serviceCollection
           .Configure<MockDataOptions>(configuration.GetSection("MockJson"));

        internal static IServiceCollection AddCorsPolicies(this IServiceCollection serviceCollection)
        => serviceCollection.AddCors(options => options.AddDefaultPolicy(
            policy => {

                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            })
        );
    }
}

