using FoodManager.Internal.Shared.Http.Auth.Models;
using Mattioli.Configurations.Models;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Result<TokenDetails>> LoginAsync(string username, string password, CancellationToken cancellationToken);
        Task<Result<int>> GetTotalAsync(CancellationToken cancellationToken);
    }
}