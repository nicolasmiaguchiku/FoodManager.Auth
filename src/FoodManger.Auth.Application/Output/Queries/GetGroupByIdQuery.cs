using FoodManager.Internal.Shared.Http.Auth.Responses;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetGroupByIdQuery(Guid Id) : IQuery<Result<GroupResponse>>;