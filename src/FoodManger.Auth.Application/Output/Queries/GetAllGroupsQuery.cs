using FoodManager.Auth.Domain.Models;
using LiteBus.Queries.Abstractions;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetAllGroupsQuery() : IQuery<Result<IEnumerable<Group>>>;