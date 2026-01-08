using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Http.Auth.Responses;

namespace FoodManager.Auth.Application.Mappers
{
    public static class TokenMapper
    {
        public static TokenDetailsResponse ToTokenResponse(this TokenDetails token)
        {
            return new (
                token.Access_Token,
                token.Expires_In,
                token.Expires_In,
                token.Refresh_Token,
                token.Token_Type,
                token.Scope);
        }
    }
}