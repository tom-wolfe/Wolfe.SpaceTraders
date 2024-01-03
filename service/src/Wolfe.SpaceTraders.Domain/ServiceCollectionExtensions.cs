using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolfe.SpaceTraders.Domain.Marketplaces;

namespace Wolfe.SpaceTraders.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomainLayer(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddSingleton<IMarketPriorityService, MarketPriorityService>();
    }
}