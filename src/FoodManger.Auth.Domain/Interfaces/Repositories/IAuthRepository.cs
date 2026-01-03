using FoodManager.Auth.Domain.Models;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<Result<TokenDetails>> GetAccessTokenAsync(CancellationToken cancellationToken);
    }
}
