using FoodManager.Auth.Domain.Models;

namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IGroupRepository : IBaseRepository
    {
        Task<Result<IEnumerable<Group>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<Group>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Result> CreateAsync(string name, Dictionary<string, string[]> attributes, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}