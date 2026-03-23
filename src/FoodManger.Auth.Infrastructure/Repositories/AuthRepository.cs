using Flurl;
using FoodManager.Auth.Domain.Errors;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;
using System.Text.Json;

namespace FoodManager.Auth.Infrastructure.Repositories
{
    public class AuthRepository(IHttpClientFactory httpClientFactory, IKeycloakSettings keycloakSettings) : IAuthRepository
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient("KeycloakClient");

        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<Result<TokenDetails>> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            var requestData = new FormUrlEncodedContent(
                [
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_secret", keycloakSettings.ClientSecret),
                    new KeyValuePair<string, string>("client_id", keycloakSettings.ClientId)
                ]);

            var url = _httpClient.BaseAddress
                .AppendPathSegment("realms")
                .AppendPathSegment(keycloakSettings.RealmName)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("token");

            var response = await _httpClient.PostAsync(url, requestData, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                UserErrors.SetTechnicalMessage(content);
                return Result<TokenDetails>.Failure(UserErrors.TokenGenerationError);
            }

            var result = JsonSerializer.Deserialize<TokenDetails>(content, jsonOptions)!;

            return Result<TokenDetails>.Success(result);
        }
    }
}