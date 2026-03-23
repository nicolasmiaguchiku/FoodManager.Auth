using Flurl;
using FoodManager.Auth.Domain.Entities;
using FoodManager.Auth.Domain.Errors;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Responses;
using System.Text.Json;

namespace FoodManager.Auth.Infrastructure.Repositories
{
    public class GroupUsersRepository(IHttpClientFactory httpClientFactory, IAuthRepository _authRepository, IKeycloakSettings keycloakSettings) : BaseRepository(httpClientFactory), IGroupUsersRepository
    {
        private JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Result<IEnumerable<User>>> GetUsersInGroupAsync(Guid id, CancellationToken cancellationToken)
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
                GroupErrors.SetTechnicalMessage(response.ReasonPhrase!);
                return Result<IEnumerable<User>>.Failure(GroupErrors.GetUsersInGroupsError);
            }
            var users = JsonSerializer.Deserialize<IEnumerable<User>>(content, jsonOptions);
            return Result<IEnumerable<User>>.Success(users!);
        }
    }
}
