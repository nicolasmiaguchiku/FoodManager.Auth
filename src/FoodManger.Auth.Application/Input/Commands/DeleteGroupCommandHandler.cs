using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Auth.Domain.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands
{
    public sealed class DeleteGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<DeleteGroupCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(DeleteGroupCommand message, CancellationToken cancellationToken = default)
        {
            var result = await groupRepository.DeleteAsync(message.Id, cancellationToken);

            if (result.IsFailure)
            {
                return Result<bool>.Failure(result.Error);

            }
            return Result<bool>.Success(true);
        }
    }
}