using FoodManager.Internal.Shared.Filters;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Http.Auth.Responses;
using FoodManager.Internal.Shared.Responses;

namespace FoodManager.Auth.Application.Mappers
{
    public static class GroupMapper
    {
        public static GroupResponse ToResponse(this Group group)
        {
            return new GroupResponse(group.Id, group.Name, group.Path, group.Description!, group.Attributes!);
        }

        public static PagedResult<UserGroupResponse> ToResponse(this UserGroupResponse results, PageFilterRequest pageFilter, int totalResults)
        {
            return new PagedResult<UserGroupResponse>
            {
                PageNumber = pageFilter.Page,
                PageSize = pageFilter.PageSize,
                Results = [results],
                TotalResults = totalResults
            };
        }
    }
}
