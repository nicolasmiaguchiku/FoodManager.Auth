using FoodManager.Auth.CrossCutting.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace FoodManager.Auth.CrossCutting.Extentions
{
    public static class KeycloakJwtAuthenticationExtension
    {
        public static IServiceCollection AddKeycloakJwtAuthentication(this IServiceCollection services, KeycloakSettings keycloakSettings)
        {
            var authority = $"{keycloakSettings.BaseUrl.TrimEnd('/')}/realms/{keycloakSettings.Realm}";
            var authorityUri = new Uri(authority);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = authority;
                    options.Audience = keycloakSettings.ClientId;

                    options.RequireHttpsMetadata = authorityUri.Scheme == Uri.UriSchemeHttps;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }

    }
}