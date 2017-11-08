using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookStore.Auth
{
    public static class AuthServiceCollectionExtensions
    {

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            const string issuer = "http://localhost:5000";
            const string token = "tokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentokentoken";

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
                    ValidateAudience = false,
                    ValidIssuer = issuer
                };
            });

            services.Configure<SecurityTokenOptions>(opt =>
            {
                opt.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)), SecurityAlgorithms.HmacSha256);
                opt.Issuer = issuer;
            });

            services.AddTransient<JwtSecurityTokenHandler>();

            return services;
        }
    }
}
