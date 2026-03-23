using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands;

public record AddGroupCommand(AddGroupRequest AddGroupRequest) : ICommand<Result<bool>>;