using FoodManager.Internal.Shared.Http.Auth.Models;
using LiteBus.Queries.Abstractions;
using Mattioli.Configurations.Models;

namespace FoodManager.Auth.Application.Output.Queries;

public record GetAllGroupsQuery() : IQuery<Result<IEnumerable<Group>>>;