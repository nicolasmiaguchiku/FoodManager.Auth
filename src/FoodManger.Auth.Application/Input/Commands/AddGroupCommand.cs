using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Auth.Domain.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands;

public record AddGroupCommand(AddGroupRequest AddGroupRequest) : ICommand<Result<bool>>;