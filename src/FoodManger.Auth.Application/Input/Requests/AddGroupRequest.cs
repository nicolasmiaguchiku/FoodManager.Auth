namespace FoodManager.Auth.Application.Input.Requests;

public record AddGroupRequest(string Name, Dictionary<string, string[]> Attributes);