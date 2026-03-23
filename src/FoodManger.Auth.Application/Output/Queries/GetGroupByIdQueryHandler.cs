using FoodManager.Auth.Application.Mappers;
using FoodManager.Internal.Shared.Http.Auth.Responses;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries
{
    public sealed class GetGroupByIdQueryHandler(IGroupRepository groupRepository) : IQueryHandler<GetGroupByIdQuery, Result<GroupResponse>>
    {
        public async Task<Result<GroupResponse>> HandleAsync(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var groupResult = await groupRepository.GetByIdAsync(request.Id, cancellationToken);

            if (groupResult.IsFailure)
            {
                return Result<GroupResponse>.Failure(groupResult.Error);
            }

            return Result<GroupResponse>.Success(groupResult.Data.ToResponse());
        }
    }
}