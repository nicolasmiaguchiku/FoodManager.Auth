using FoodManager.Auth.Domain.Filters;

namespace FoodManager.Auth.Application.Input.Requests
{
    public class GetUsersGroupRequest
    {
        public PageFilterRequest PageFilter { get; set; }
        public Guid GroupId { get; set; }
        public IEnumerable<string>? Usernames { get; set; }

        public GetUsersGroupRequest()
        {
            PageFilter = new PageFilterRequest
            {
                Page = 1,
                PageSize = 60
            };
        }
    }
}