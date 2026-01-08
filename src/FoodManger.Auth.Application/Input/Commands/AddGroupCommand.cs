using FoodManager.Auth.Application.Input.Requests;
using Mattioli.Configurations.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands;

public record AddGroupCommand(AddGroupRequest AddGroupRequest) : ICommand<Result<bool>>;