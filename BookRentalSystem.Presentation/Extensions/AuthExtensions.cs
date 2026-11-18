using System.Text;
using BookRentalSystem.Business.Options;
using BookRentalSystem.DataAccess.Models;
using BookRentalSystem.Presentation.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BookRentalSystem.Presentation.Extensions
{
    public static class AuthExtensions
    {
        private const string JwtSectionName = "JwtSettings";

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection(JwtSectionName);
            services.Configure<JwtOptions>(jwtSection);

            var jwtOptions = jwtSection.Get<JwtOptions>();
            if (jwtOptions == null || string.IsNullOrWhiteSpace(jwtOptions.Key))
            {
                throw new InvalidOperationException("JWT settings are not configured properly.");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                });

            return services;
        }

        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.AdminOnly, policy => policy.RequireRole(Role.Admin, Role.SuperAdmin));
                options.AddPolicy(Policies.SuperAdminOnly, policy => policy.RequireRole(Role.SuperAdmin));
                options.AddPolicy(Policies.UserOnly, policy => policy.RequireRole(Role.User));
            });

            return services;
        }
    }
}
