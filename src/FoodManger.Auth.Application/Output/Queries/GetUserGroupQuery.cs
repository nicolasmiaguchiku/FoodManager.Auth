using FoodManager.Auth.Application.Input.Requests;
using FoodManager.Auth.Application.Output.Responses;
using FoodManager.Auth.Domain.Models;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetUsersGroupQuery(GetUsersGroupRequest GetUsersGroupRequest) : IQuery<Result<PagedResult<UserGroupResponse>>>;