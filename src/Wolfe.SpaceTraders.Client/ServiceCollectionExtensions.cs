using Microsoft.Extensions.Options;
using Refit;
using Wolfe.SpaceTraders.Token;

namespace Wolfe.SpaceTraders;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpaceTradersClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<SpaceTradersOptions>()
            .Configure(configuration.GetSection("SpaceTraders").Bind)
            .Validate(o => !string.IsNullOrEmpty(o.ApiBaseUri), $"{nameof(SpaceTradersOptions.ApiBaseUri)} cannot be null or empty.");

        services
            .AddRefitClient<ISpaceTradersApiClient>(provider => new RefitSettings
            {
                AuthorizationHeaderValueGetter = async (_, ct) =>
                {
                    var tokenService = provider.GetRequiredService<ITokenReader>();
                    var token = await tokenService.Read(ct);
                    return token ?? "";
                }
            })
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<SpaceTradersOptions>>().Value;
                client.BaseAddress = new Uri(options.ApiBaseUri);
            });

        var tokenService = new FileTokenService();
        return services
            .AddSingleton<ISpaceTradersClient, SpaceTradersClient>()
            .AddSingleton<ITokenReader>(tokenService)
            .AddSingleton<ITokenWriter>(tokenService);
    }
}