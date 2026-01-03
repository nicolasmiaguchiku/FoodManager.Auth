namespace FoodManager.Auth.CrossCutting.Models
{
    public class KeycloakSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public Realm Realm { get; set; } = null!;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}