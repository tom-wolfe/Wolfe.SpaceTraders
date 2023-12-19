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
            .AddSpaceTradersApi(o =>
            {
                configuration.GetSection("SpaceTraders").Bind(o);
                o.ApiKeyProvider = async (x, ct) =>
                {
                    var y = await tokenService.Read(ct);
                    return y ?? throw new InvalidOperationException("Missing API token.");
                };
            })
            .AddSingleton<ISpaceTradersClient, SpaceTradersClient>()
            .AddSingleton<ITokenReader>(tokenService)
            .AddSingleton<ITokenWriter>(tokenService);
    }
}