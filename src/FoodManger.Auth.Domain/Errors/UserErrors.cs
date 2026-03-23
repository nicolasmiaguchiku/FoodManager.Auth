using FoodManager.Internal.Shared.Responses;

namespace FoodManager.Auth.Domain.Errors
{
    public static class UserErrors
    {
        public static string TechnicalMessage { get; private set; } = "";

        public static Error InvalidUserNameOrPasswordError => new(
            "User.InvalidUserNameOrPassword",
            $"An error occurred while trying to get JWT token. Please check username and password. {TechnicalMessage}"
        );

        public static Error TokenGenerationError => new(
           "User.TokenGenerationError",
           $"An error occurred while trying to receive information about realm: {TechnicalMessage}"
       );

        public static Error GetAllUsersError => new(
            "User.GetAllUsersError",
            $"An error occurred while trying get all users. {TechnicalMessage}"
        );

        public static void SetTechnicalMessage(string technicalMessage)
        {
            TechnicalMessage = technicalMessage;
        }
    }
}
