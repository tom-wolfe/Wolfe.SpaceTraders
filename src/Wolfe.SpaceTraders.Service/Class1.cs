using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Wolfe.SpaceTraders.Service;

public static class ClientCollectionExtensions
{
    public static IServiceCollection AddServiceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<ISpaceTradersService, SpaceTradersService>();
    }

}