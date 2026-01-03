namespace FoodManager.Auth.Domain.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        public HttpClient CreateHttpClientWithHeaders(string accessToken);
    }
}