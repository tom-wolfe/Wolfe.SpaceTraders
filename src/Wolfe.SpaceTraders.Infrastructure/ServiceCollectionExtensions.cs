using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenService = new FileTokenService();
        return services
            .AddSpaceTradersApi(configuration.GetSection("SpaceTraders").Bind)
            .AddSingleton<ISpaceTradersClient, SpaceTradersClient>()
            .AddSingleton<ITokenReader>(tokenService)
            .AddSingleton<ITokenWriter>(tokenService);
    }
}