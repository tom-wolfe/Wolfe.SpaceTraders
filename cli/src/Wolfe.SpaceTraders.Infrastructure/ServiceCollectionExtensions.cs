using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSpaceTradersApi(o =>
            {
                configuration.GetSection("SpaceTraders").Bind(o);
                o.ApiKeyProvider = async (provider, ct) =>
                {
                    var tokenService = provider.GetRequiredService<ITokenService>();
                    var token = await tokenService.Read(ct);
                    return token ?? throw new InvalidOperationException("Missing API token.");
                };
            })
            .AddSingleton<ISpaceTradersClient, SpaceTradersClient>()
            .AddSingleton<ITokenService, FileTokenService>();
    }
}