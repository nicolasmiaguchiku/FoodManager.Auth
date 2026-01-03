using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Auth.Domain.Models;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries
{
    public sealed class GetAllGroupsQueryHandler(IGroupRepository groupRepository) : IQueryHandler<GetAllGroupsQuery, Result<IEnumerable<Group>>>
    {
        public async Task<Result<IEnumerable<Group>>> HandleAsync(GetAllGroupsQuery message, CancellationToken cancellationToken = default)
        {
            var result = await groupRepository.GetAllAsync(cancellationToken);

            return Result<IEnumerable<Group>>.Success(result.Data);
        }
    }
}