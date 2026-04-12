using FoodManager.Auth.Domain.Entities;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Result<TokenDetails>> LoginAsync(string username, string password, CancellationToken cancellationToken);
        Task<Result<bool>> SignoutAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Result<int>> GetTotalAsync(CancellationToken cancellationToken);
        Task<Result<bool>> ResetPassword(Guid Id, string resetPassword, CancellationToken cancellationToken);
        Task<Result<string>> CreteUserAsync(UserEntity user, CancellationToken cancellationToken);
    }
}