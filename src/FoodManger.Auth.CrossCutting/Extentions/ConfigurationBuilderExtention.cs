using FoodManager.Auth.CrossCutting.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace FoodManager.Auth.CrossCutting.Extentions
{
    public static class ConfigurationBuilderExtention
    {
        public static Settings ApplyEnvironmentOverridesToSettings(this IConfiguration configuration, IHostEnvironment host)
        {
            var settings = configuration.GetSection("Settings").Get<Settings>();

            if (!host.IsDevelopment())
            {
                settings!.MongoSettings.ConnectionString = GetOrDefault("FoodManager.ConnectionString", settings.MongoSettings.ConnectionString);
                settings!.MongoSettings.Database = GetOrDefault("FoodManager.DataBaseName", settings.MongoSettings.Database);
                settings!.KeycloakSettings.BaseUrl = GetOrDefault("FoodManager.Keycloak.BaseUrl", settings.KeycloakSettings.BaseUrl);
                settings!.KeycloakSettings.Realm.Name = GetOrDefault("FoodManager.Keycloak.Real.Name", settings.KeycloakSettings.Realm.Name);
                settings!.KeycloakSettings.ClientId = GetOrDefault("FoodManager.Keycloak.ClientId", settings.KeycloakSettings.ClientId);
                settings!.KeycloakSettings.ClientSecret = GetOrDefault("FoodManager.Keycloak.ClientSecret", settings.KeycloakSettings.ClientSecret);
            }

            return settings!;
        }

        public static string GetOrDefault(string key, string? fallback)
        {
            var value = Environment.GetEnvironmentVariable(key);
            return string.IsNullOrWhiteSpace(value) ? fallback ?? "" : value;
        }
    }
}
