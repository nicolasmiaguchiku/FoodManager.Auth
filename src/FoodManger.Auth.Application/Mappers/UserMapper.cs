using FoodManager.Auth.Application.Output.Responses;
using FoodManager.Auth.Domain.Models;


namespace FoodManager.Auth.Application.Mappers
{
    public static class UserMapper
    {
        //public static UserFilters ToUserFilters(this GetUsersGroupRequest getUsersRequest)
        //{
        //    var pageFilter = new PageFilter(getUsersRequest.PageFilter.Page, getUsersRequest.PageFilter.PageSize);
        //    return new UserFilters(pageFilter, [], getUsersRequest.Usernames);
        //}

        public static IEnumerable<UserResponse> ToUsersResponse(this IEnumerable<User> users)
        {
            return users.Select(x => new UserResponse(x.Id,
                x.Enabled,
                x.EmailVerified,
                x.Username,
                x.Email,
                x.FirstName ?? "",
                x.LastName!,
                x.Totp,
                x.DisableableCredentialTypes,
                x.RequiredActions,
                x.NotBefore,
                x.CreatedTimestamp,
                x.Access?.ToAcess(),
                x.Attributes));
        }

        public static Access ToAcess(this Access access)
        {
            return new Access
            {
                Impersonate = access.Impersonate,
                Manage = access.Manage,
                ManageGroupMembership = access.ManageGroupMembership,
                MapRoles = access.MapRoles,
                View = access.View,
            };
        }
    }
}