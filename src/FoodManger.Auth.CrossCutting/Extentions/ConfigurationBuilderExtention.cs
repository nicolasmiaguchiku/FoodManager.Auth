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
                settings!.MongoSettings.ConnectionString = GetOrDefault("ConnectionString_Mongo", settings.MongoSettings.ConnectionString);
                settings.MltSettings.SeqUrl = GetOrDefault("SeqUrl", settings.MltSettings.SeqUrl);
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
