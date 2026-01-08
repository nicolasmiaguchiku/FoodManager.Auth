using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Internal.Shared.Http.Auth.Responses;
using Mattioli.Configurations.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands;

public record LoginCommand(LoginRequest Request) : ICommand<Result<TokenDetailsResponse>>;