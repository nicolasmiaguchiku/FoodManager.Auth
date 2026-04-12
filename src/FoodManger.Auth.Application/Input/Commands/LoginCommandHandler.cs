using FoodManager.Auth.Domain.Interfaces.Repositories;
using FoodManager.Internal.Shared.Http.Auth.Models;
using FoodManager.Internal.Shared.Responses;
using LiteBus.Commands.Abstractions;
using Microsoft.Extensions.Logging;

namespace FoodManager.Auth.Application.Input.Commands
{
    public sealed class LoginCommandHandler(IUserRepository userRepository, ILogger<LoginCommandHandler> logger) : ICommandHandler<LoginCommand, Result<TokenDetails>>
    {
        public async Task<Result<TokenDetails>> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
        {
            return await userRepository.LoginAsync(command.Request.Username, command.Request.Password, cancellationToken);
        }
    }
}