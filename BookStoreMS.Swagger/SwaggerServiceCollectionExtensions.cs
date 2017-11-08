using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Swagger
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, Action<SwaggerGenOptions> action)
        {

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

                action(options);
            });

            return services;
        }
    }
}
