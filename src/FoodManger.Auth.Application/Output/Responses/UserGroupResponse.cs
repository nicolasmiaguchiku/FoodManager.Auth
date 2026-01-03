namespace FoodManager.Auth.Application.Output.Responses;

public record UserGroupResponse(GroupResponse Group, IEnumerable<UserResponse> Users);