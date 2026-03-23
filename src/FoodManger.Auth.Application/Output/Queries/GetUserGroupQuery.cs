using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Internal.Shared.Http.Auth.Responses;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetUsersGroupQuery(GetUsersGroupRequest GetUsersGroupRequest) : IQuery<Result<PagedResult<UserGroupResponse>>>;