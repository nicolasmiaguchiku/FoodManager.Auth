using Flurl;
using FoodManager.Auth.Domain.Errors;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace FoodManager.Auth.Infrastructure.Repositories
{
    public class GroupRepository(
        IHttpClientFactory httpClientFactory,
        IAuthRepository _authRepository,
        IKeycloakSettings keycloakSettings,
        ILogger<GroupRepository> logger) : BaseRepository(httpClientFactory), IGroupRepository
    {
        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        public async Task<Result<IEnumerable<Group>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var tokenDetailsResult = await _authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetailsResult.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(keycloakSettings.RealmName)
                    .AppendPathSegment("groups");

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

                return Result<IEnumerable<Group>>.Failure(GroupErrors.GetGroupsError);
            }

            var result = JsonSerializer.Deserialize<IEnumerable<Group>>(content, jsonOptions)!;

            return Result<IEnumerable<Group>>.Success(result);
        }

        public async Task<Result<Group>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var tokenDetailsResult = await _authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetailsResult.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(keycloakSettings.RealmName)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(id);

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

                return Result<Group>.Failure(GroupErrors.GetGroupByIdError);
            }
            var result = JsonSerializer.Deserialize<Group>(content, jsonOptions)!;

            return Result<Group>.Success(result);
        }

        public async Task<Result> CreateAsync(string name, Dictionary<string, string[]> attributes, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);

            if (tokenDetails.IsFailure)
            {
                return Result.Failure(tokenDetails.Error);
            }

            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(keycloakSettings.RealmName)
                    .AppendPathSegment("groups");

            var group = new
            {
                name,
                attributes
            };

            var jsonContent = JsonSerializer.Serialize(group);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = $"StatusCode {response.StatusCode} - ReasonPhrase {response.ReasonPhrase} - Response {content}";
                GroupErrors.SetTechnicalMessage(message);

                logger.LogError("Auth - StatusCode {statusCode} - ReasonPhrase - {ReasonPhrase} - Response - {Response}",
                    response.StatusCode,
                    response.ReasonPhrase,
                    content);

                return Result.Failure(GroupErrors.CreationGroupError);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var tokenDetails = await _authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                    .AppendPathSegment("admin")
                    .AppendPathSegment("realms")
                    .AppendPathSegment(keycloakSettings.RealmName)
                    .AppendPathSegment("groups")
                    .AppendPathSegment(id);

            var response = await httpClient.DeleteAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = $"StatusCode {response.StatusCode} - ReasonPhrase {response.ReasonPhrase}";
                GroupErrors.SetTechnicalMessage(message);

                logger.LogError("Auth - StatusCode {statusCode} - ReasonPhrase - {ReasonPhrase}",
                    response.StatusCode,
                    response.ReasonPhrase);

                return Result.Failure(GroupErrors.DeletionGroupError);
            }

            return Result.Success();
        }
    }
}