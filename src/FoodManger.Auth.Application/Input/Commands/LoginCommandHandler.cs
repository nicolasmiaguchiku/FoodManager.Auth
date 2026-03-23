using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Commands.Abstractions;

namespace FoodManager.Auth.Application.Input.Commands
{
    public sealed class LoginCommandHandler(IUserRepository userRepository) : ICommandHandler<LoginCommand, Result<TokenDetails>>
    {
        public async Task<Result<TokenDetails>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
        {
            var tokenResult = await userRepository.LoginAsync(command.Request.Username, command.Request.Password, cancellationToken);

            return Result<TokenDetails>.Success(tokenResult.Data);
        }
    }
}