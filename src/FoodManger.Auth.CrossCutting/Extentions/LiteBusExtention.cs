using FoodManager.Auth.Application.Input.Commands;
using FoodManager.Auth.Application.Output.Queries;
using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;
using LiteBus.Queries.Extensions.MicrosoftDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace FoodManager.Auth.CrossCutting.Extentions
{
    public static class LiteBusExtention
    {
        public static IServiceCollection ConfigureLiteBus(this IServiceCollection service)
        {
            service.AddLiteBus(litebus =>
            {
                litebus.AddCommandModule(module =>
                {
                    module.RegisterFromAssembly(typeof(LoginCommand).Assembly);
                });

                litebus.AddQueryModule(module =>
                {
                    module.RegisterFromAssembly(typeof(GetAllGroupsQuery).Assembly);
                });
            });

            return service;
        }
    }
}