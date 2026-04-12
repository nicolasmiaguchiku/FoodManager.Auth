using FoodManager.Internal.Shared.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodManager.Auth.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class KeycloakSettingsEntity : KeycloakSettings
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}