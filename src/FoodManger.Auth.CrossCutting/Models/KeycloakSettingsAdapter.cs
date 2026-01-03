using FoodManager.Auth.Domain.Interfaces.Repositories;

namespace FoodManager.Auth.CrossCutting.Models
{
    public class KeycloakSettingsAdapter(KeycloakSettings settings) : IKeycloakSettings
    {
        private readonly KeycloakSettings _settings = settings;

        public string BaseUrl => _settings.BaseUrl;
        public string RealmName => _settings.Realm.Name;
        public string ClientId => _settings.ClientId;
        public string ClientSecret => _settings.ClientSecret;
    }
}