namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IKeycloakSettings
    {
        string BaseUrl { get; }
        string RealmName { get; }
        string ClientId { get; }
        string ClientSecret { get; }
    }
}