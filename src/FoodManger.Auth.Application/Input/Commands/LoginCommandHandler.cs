using FoodManager.Auth.Application.Mappers;
using FoodManager.Auth.Application.Output.Responses;
using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Auth.Domain.Models;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands
{
    public sealed class LoginCommandHandler(IUserRepository userRepository) : ICommandHandler<LoginCommand, Result<TokenDetailsResponse>>
    {
        public async Task<Result<TokenDetailsResponse>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
        {
            var tokenResult = await userRepository.LoginAsync(command.Request.Username, command.Request.Password, cancellationToken);

            return Result<TokenDetailsResponse>.Success(tokenResult.Data.ToTokenResponse());
        }
    }
}