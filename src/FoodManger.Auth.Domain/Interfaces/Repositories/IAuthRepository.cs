using Mattioli.Configurations.Models;
using FoodManager.Internal.Shared.Http.Auth.Models;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<Result<TokenDetails>> GetAccessTokenAsync(CancellationToken cancellationToken);
    }
}
