using FoodManager.Internal.Shared.Http.Auth.Models;
using LiteBus.Queries.Abstractions;
using FoodManager.Internal.Shared.Responses;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetAllGroupsQuery() : IQuery<Result<IEnumerable<Group>>>;