using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Http.Auth.Responses;

namespace FoodManager.Auth.Application.Mappers
{
    public static class TokenMapper
    {
        public static TokenDetailsResponse ToTokenResponse(this TokenDetails token)
        {
            return new TokenDetailsResponse {
                AccessToken = token.Access_Token,
                ExpiresIn = token.Expires_In,
                RefreshExpiresIn = token.Refresh_Expires_In,
                RefreshToken = token.Refresh_Token,
                TokenType = token.Token_Type,
                Scope = token.Scope
            };
        }
    }
}