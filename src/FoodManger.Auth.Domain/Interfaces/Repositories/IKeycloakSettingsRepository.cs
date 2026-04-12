using FoodManager.Auth.Domain.Entities;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IKeycloakSettingsRepository
    {
        Task<bool> AddConfigAsync(KeycloakSettingsEntity newConfig, CancellationToken cancellationToken);
        Task<KeycloakSettingsEntity> GetConfigAsync();
        Task<bool> UpdateRealmConfigAsync(Guid id, KeycloakSettingsEntity keycloakSettings);
    }
}