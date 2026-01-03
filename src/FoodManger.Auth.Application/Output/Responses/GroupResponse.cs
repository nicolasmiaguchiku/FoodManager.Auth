namespace FoodManager.Auth.Application.Output.Responses;

public record GroupResponse(Guid Id, string Name, string Path, string Description, Dictionary<string, string[]> Attributes);