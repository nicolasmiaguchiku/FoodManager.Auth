namespace FoodManager.Auth.CrossCutting.Models
{
    public class Realm
    {
        public bool DefaultSwaggerTokenGeneration { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Issuer { get; set; } = string.Empty;

        public string? Audience { get; set; }
    }
}
