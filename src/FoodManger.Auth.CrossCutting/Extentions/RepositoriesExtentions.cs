using FoodManager.Auth.CrossCutting.Models;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Auth.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Auth.CrossCutting.Extentions
{
    public static class RepositoriesExtentions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, Settings settings)
        {
            services.AddSingleton<IKeycloakSettingsRepository>(new KeycloakSettingsAdapter(settings.KeycloakSettings));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupUsersRepository, GroupUsersRepository>();

            //services.AddScoped<IClientRepository, ClientRepository>();
            //services.AddScoped<IClientScopesRepository, ClientScopesRepository>();

            return services;
        }
    }
}