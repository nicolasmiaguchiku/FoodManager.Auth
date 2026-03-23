using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Models;
using LiteBus.Queries.Abstractions;
using FoodManager.Internal.Shared.Responses;

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