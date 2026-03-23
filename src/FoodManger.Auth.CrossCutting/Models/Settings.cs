using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Models;

namespace FoodManager.Auth.CrossCutting.Models
{
    interface ISettings
    {
        public MongoSettings MongoSettings { get; set; }
        public KeycloakSettings KeycloakSettings { get; set; }
        public MltSettings MltSettings { get; set; }
    }

    public class Settings : ISettings
    {
        public required MongoSettings MongoSettings { get; set; }
        public required KeycloakSettings KeycloakSettings { get; set; }
        public required MltSettings MltSettings { get; set; }
    }
}