using Flurl;
using FoodManager.Auth.Domain.Entities;
using FoodManager.Auth.Domain.Errors;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Responses;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FoodManager.Auth.Infrastructure.Repositories
{
    public class GroupUsersRepository(
        IHttpClientFactory httpClientFactory,
        IAuthRepository _authRepository,
        IKeycloakSettings keycloakSettings,
        ILogger<GroupRepository> logger) : BaseRepository(httpClientFactory), IGroupUsersRepository
    {
        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        public async Task<Result<IEnumerable<UserEntity>>> GetUsersInGroupAsync(Guid id, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(keycloakSettings.RealmName)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(id)
                    .AppendPathSegment("members");

            var response = await httpClient.GetAsync(url, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = $"StatusCode {response.StatusCode} - ReasonPhrase {response.ReasonPhrase} - Response {content}";
                GroupErrors.SetTechnicalMessage(message);

                logger.LogError("Auth - StatusCode {statusCode} - ReasonPhrase - {ReasonPhrase} - Response - {Response}",
                    response.StatusCode,
                    response.ReasonPhrase,
                    content);

                return Result<IEnumerable<UserEntity>>.Failure(GroupErrors.GetUsersInGroupsError);
            }
            var users = JsonSerializer.Deserialize<IEnumerable<UserEntity>>(content, jsonOptions)!;

            return Result<IEnumerable<UserEntity>>.Success(users);
        }
    }
}