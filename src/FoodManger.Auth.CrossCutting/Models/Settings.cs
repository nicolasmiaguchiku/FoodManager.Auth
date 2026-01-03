namespace FoodManager.Auth.CrossCutting.Models
{
    interface ISettings
    {
        public MongoSettings MongoSettings { get; set; }
        public KeycloakSettings KeycloakSettings { get; set; }
    }

    public class Settings : ISettings
    {
        public required MongoSettings MongoSettings { get; set; }
        public required KeycloakSettings KeycloakSettings { get; set; }
    }
}
