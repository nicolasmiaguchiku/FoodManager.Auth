using Mattioli.Configurations.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands;

public record class DeleteGroupCommand(Guid Id) : ICommand<Result<bool>>;