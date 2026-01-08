using FoodManager.Internal.Shared.Http.Auth.Responses;
using Mattioli.Configurations.Models;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetGroupByIdQuery(Guid Id) : IQuery<Result<GroupResponse>>;