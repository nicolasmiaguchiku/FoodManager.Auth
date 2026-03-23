using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Http.Auth.Responses;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands;

public record LoginCommand(LoginRequest Request) : ICommand<Result<TokenDetails>>;