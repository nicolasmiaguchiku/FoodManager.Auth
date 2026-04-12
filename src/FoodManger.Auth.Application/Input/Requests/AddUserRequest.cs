namespace FoodManager.Auth.Application.Input.Requests;

public record AddUserRequest(
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName,
    Dictionary<string, string[]> Attributes);