using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Auth.Application.Output.Responses;
using FoodManager.Auth.Domain.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands;

public record LoginCommand(LoginRequest Request) : ICommand<Result<TokenDetailsResponse>>;