using Flurl;
using FoodManager.Auth.Domain.Errors;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;
using System.Text.Json;

namespace FoodManager.Auth.Infrastructure.Repositories
{
    public class UserRepository(IHttpClientFactory httpClientFactory, IKeycloakSettings keycloakSettings, IAuthRepository authRepository) : BaseRepository(httpClientFactory), IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Result<int>> GetTotalAsync(CancellationToken cancellationToken)
        {
            var tokenDetails = await authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var urlGetUsers = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(keycloakSettings.RealmName)
                .AppendPathSegment("users")
                .SetQueryParam("first", 0)
                .SetQueryParam("max", 99999);

            var response = await httpClient.GetAsync(urlGetUsers, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                UserErrors.SetTechnicalMessage(response.ReasonPhrase!);
                return Result<int>.Failure(UserErrors.GetAllUsersError);
            }

            var users = JsonSerializer.Deserialize<IEnumerable<Domain.Entities.User>>(content)!;

            return Result<int>.Success(users.Count());
        }

        public async Task<Result<TokenDetails>> LoginAsync(string username, string password, CancellationToken cancellationToken)
        {
            using var httpClient = _httpClientFactory.CreateClient("KeycloakClient");

            var UrlGetToken = httpClient.BaseAddress
                .AppendPathSegment("realms")
                .AppendPathSegment(keycloakSettings.RealmName)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var requestData = new FormUrlEncodedContent(
                [
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", keycloakSettings.ClientId),
                    new KeyValuePair<string,string>("client_secret", keycloakSettings.ClientSecret),
                    new KeyValuePair<string, string>("username", username),
                    new KeyValuePair<string, string>("password", password)
                ]);

            var response = await httpClient.PostAsync(UrlGetToken, requestData, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                UserErrors.SetTechnicalMessage(response.ReasonPhrase!);
                return Result<TokenDetails>.Failure(UserErrors.InvalidUserNameOrPasswordError);
            }

            var result = JsonSerializer.Deserialize<TokenDetails>(content, jsonOptions);

            if (result is null)
            {
                return Result<TokenDetails>.Failure(UserErrors.InvalidUserNameOrPasswordError);
            }

            return Result<TokenDetails>.Success(result);
        }
    }
}
