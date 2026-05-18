namespace FoodManager.Auth.Domain.Entities
{
    public class RealmEntity
    {
        public required string Realm { get; set; }
        public string RealmName { get; set; } = string.Empty;
        public bool Enabled { get; set; }
    }
}