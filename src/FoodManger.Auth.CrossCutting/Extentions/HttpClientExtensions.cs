using FoodManager.Auth.CrossCutting.Models;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Auth.CrossCutting.Extentions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, KeycloakSettings keycloakSettings)
        {
            services.AddHttpClient("KeycloakClient", client =>
            {
                client.BaseAddress = new Uri(keycloakSettings.BaseUrl);
            });

            return services;
        }
    }
}
