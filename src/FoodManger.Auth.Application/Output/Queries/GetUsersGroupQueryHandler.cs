using FoodManager.Auth.Application.Mappers;
using FoodManager.Auth.Domain.Errors;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Responses;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries
{
    public sealed class GetUsersGroupQueryHandler(IGroupUsersRepository groupUsersRepository, IGroupRepository groupRepository) : IQueryHandler<GetUsersGroupQuery, Result<PagedResult<UserGroupResponse>>>
    {
        public async Task<Result<PagedResult<UserGroupResponse>>> HandleAsync(GetUsersGroupQuery request, CancellationToken cancellationToken = default)
        {
            var allGroupsResult = await groupRepository.GetAllAsync(cancellationToken);

            if (allGroupsResult.IsSuccess)
            {
                var groupSearched = allGroupsResult.Data.FirstOrDefault(x => x.Id == request.GetUsersGroupRequest.GroupId);

                if (groupSearched != null)
                {
                    var resultMembers = await groupUsersRepository.GetUsersInGroupAsync(
                        groupSearched.Id,
                        cancellationToken);

                    var filteredUsers = resultMembers.Data.AsEnumerable();

                    if (request.GetUsersGroupRequest.Usernames?.Any() ?? false)
                    {
                        filteredUsers = resultMembers.Data
                            .Where(x => request.GetUsersGroupRequest.Usernames.Any(filter => x.Username.Contains(filter, StringComparison.OrdinalIgnoreCase)));
                    }

                    var usersInGroup = new UserGroupResponse(groupSearched.ToResponse(), filteredUsers.ToUsersResponse());

                    var result = usersInGroup.ToResponse(request.GetUsersGroupRequest.PageFilter, filteredUsers.Count());

                    return Result<PagedResult<UserGroupResponse>>.Success(result);
                }
            }

            return Result<PagedResult<UserGroupResponse>>.Failure(GroupErrors.GetUsersInGroupsError);
        }
    }
}
