using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Internal.Shared.Http.Auth.Responses;
using LiteBus.Queries.Abstractions;
using Mattioli.Configurations.Http;
using Mattioli.Configurations.Models;


namespace FoodManager.Auth.Application.Output.Queries;

public record GetUsersGroupQuery(GetUsersGroupRequest GetUsersGroupRequest) : IQuery<Result<PagedResult<UserGroupResponse>>>;