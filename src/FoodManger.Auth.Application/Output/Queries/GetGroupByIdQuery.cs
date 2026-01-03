using FoodManager.Auth.Application.Output.Responses;
using FoodManager.Auth.Domain.Models;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetGroupByIdQuery(Guid Id) : IQuery<Result<GroupResponse>>;