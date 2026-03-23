using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<Result<TokenDetails>> GetAccessTokenAsync(CancellationToken cancellationToken);
    }
}
