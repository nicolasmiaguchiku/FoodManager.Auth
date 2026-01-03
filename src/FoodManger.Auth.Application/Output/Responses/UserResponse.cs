using FoodManager.Auth.Domain.Models;
using System.Text.Json.Serialization;

namespace FoodManager.Auth.Application.Output.Responses;

public record UserResponse(
    [property: JsonIgnore] Guid Id,
    bool Enabled,
    bool EmailVerified,
    string Username,
    string? Email,
    string? FirstName,
    string? LastName,
    bool Totp,
    List<string> DisableableCredentialTypes,
    List<string> RequiredActions,
    int NotBefore,
    long CreatedTimestamp,
    Access? Access,
    Dictionary<string, string[]>? Attributes);