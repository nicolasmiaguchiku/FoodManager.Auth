using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Auth.Domain.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands
{
    public class AddGroupCommandHandler(IGroupRepository groupRepository) : ICommandHandler<AddGroupCommand, Result<bool>>
    {
        public async Task<Result<bool>> HandleAsync(AddGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await groupRepository.CreateAsync(request.AddGroupRequest.Name, request.AddGroupRequest.Attributes, cancellationToken);
            if (result.IsSuccess)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure(result.Error);
        }
    }
}