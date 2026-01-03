using FoodManager.Auth.Domain.Interfaces.Repositories;
using System.Net.Http.Headers;

namespace FoodManager.Auth.Infrastructure.Repositories
{
    public class BaseRepository(IHttpClientFactory httpClientFactory) : IBaseRepository
    {
        public HttpClient CreateHttpClientWithHeaders(string accessToken)
        {
            var httpClient = httpClientFactory.CreateClient("KeycloakClient");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return httpClient;
        }
    }
}