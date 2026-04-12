using Flurl;
using FoodManager.Auth.Domain.Errors;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FoodManager.Auth.Infrastructure.Repositories
{
    public class UserRepository(
        IHttpClientFactory httpClientFactory,
        IKeycloakSettingsRepository keycloakSettings,
        IAuthRepository authRepository,
        ILogger<UserRepository> logger) : BaseRepository(httpClientFactory), IUserRepository
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        private readonly JsonSerializerOptions jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

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
                var message = $"StatusCode {response.StatusCode} - ReasonPhrase {response.ReasonPhrase} - Response {content}";
                UserErrors.SetTechnicalMessage(message);

                logger.LogError("Auth - StatusCode {statusCode} - ReasonPhrase - {ReasonPhrase} - Response - {Response}",
                    response.StatusCode,
                    response.ReasonPhrase,
                    content);

                return Result<TokenDetails>.Failure(UserErrors.InvalidUserNameOrPasswordError);
            }

            var result = JsonSerializer.Deserialize<TokenDetails>(content, jsonOptions)!;

            return Result<TokenDetails>.Success(result);
        }

        public async Task<Result<bool>> SignoutAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var httpClient = CreateHttpClientWithHeaders("KeycloakClient");

            var urlGetToken = httpClient.BaseAddress
                .AppendPathSegment("realms")
                .AppendPathSegment(keycloakSettings.RealmName)
                .AppendPathSegment("protocol")
                .AppendPathSegment("openid-connect")
                .AppendPathSegment("logout");

            var requestData = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("client_id", keycloakSettings.ClientId),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("client_secret", keycloakSettings.ClientSecret)
            ]);

            var response = await httpClient.PostAsync(urlGetToken, requestData, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = $"StatusCode {response.StatusCode} - ReasonPhrase {response.ReasonPhrase} - Response {content}";
                UserErrors.SetTechnicalMessage(message);

                logger.LogError("Auth - StatusCode {statusCode} - ReasonPhrase - {ReasonPhrase} - Response - {Response}",
                    response.StatusCode,
                    response.ReasonPhrase,
                    content);

                return Result<bool>.Failure(UserErrors.SignOutError);
            }

            return Result<bool>.Success(true);
        }

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
                var message = $"StatusCode {response.StatusCode} - ReasonPhrase {response.ReasonPhrase} - Response {content}";
                UserErrors.SetTechnicalMessage(message);

                logger.LogError("Auth - StatusCode {statusCode} - ReasonPhrase - {ReasonPhrase} - Response - {Response}",
                    response.StatusCode,
                    response.ReasonPhrase,
                    content);

                return Result<int>.Failure(UserErrors.GetAllUsersError);
            }

            var users = JsonSerializer.Deserialize<IEnumerable<User>>(content)!;

            return Result<int>.Success(users.Count());
        }

        public async Task<Result<bool>> ResetPassword(Guid Id, string resetPassword, CancellationToken cancellationToken)
        {
            var tokenDetails = await authRepository.GetAccessTokenAsync(cancellationToken);
            var httpClient = CreateHttpClientWithHeaders(tokenDetails.Data.Access_Token);

            var url = httpClient.BaseAddress
                .AppendPathSegment("admin")
                .AppendPathSegment("realms")
                .AppendPathSegment(keycloakSettings.RealmName)
                .AppendPathSegment("users")
                .AppendPathSegment(Id)
                .AppendPathSegment("reset-password");

            var passwordData = new
            {
                type = "password",
                value = resetPassword,
                temporary = false
            };

            var json = JsonSerializer.Serialize(passwordData, jsonOptions);
            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, httpContent, cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = $"StatusCode {response.StatusCode} - ReasonPhrase {response.ReasonPhrase} - Response {content}";
                UserErrors.SetTechnicalMessage(message);

                logger.LogError("Auth - StatusCode {statusCode} - ReasonPhrase - {ReasonPhrase} - Response - {Response}",
                    response.StatusCode,
                    response.ReasonPhrase,
                    content);

                return Result<bool>.Failure(UserErrors.ResetPasswordError);
            }

            return Result<bool>.Success(true);
        }
    }
}