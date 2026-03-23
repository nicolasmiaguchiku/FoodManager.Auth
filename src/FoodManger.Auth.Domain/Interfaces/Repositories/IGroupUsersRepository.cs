using FoodManager.Auth.Domain.Entities;
using FoodManager.Internal.Shared.Responses;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IGroupUsersRepository
    {
        //Task<Result<bool>> AddUserToGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);
        //Task<Result<bool>> RemoveUserFromGroupAsync(Guid userId, Guid groupId, CancellationToken cancellationToken);
        Task<Result<IEnumerable<User>>> GetUsersInGroupAsync(Guid id, CancellationToken cancellationToken);
    }
}
