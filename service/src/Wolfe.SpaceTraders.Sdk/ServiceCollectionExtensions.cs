using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System.Net;
using System.Net.Http.Json;
using Wolfe.SpaceTraders.Sdk.Responses;

namespace Wolfe.SpaceTraders.Sdk;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpaceTraders(this IServiceCollection services, Action<SpaceTradersOptions> configure)
    {
        services
            .AddOptions<SpaceTradersOptions>()
            .Configure(configure)
            .Validate(o => o.ApiKeyProvider != null, $"{nameof(SpaceTradersOptions.ApiKeyProvider)} cannot be null.");

        services
            .AddSingleton<RateLimiter>()
            .AddTransient<RateLimitingHandler>()
            .AddRefitClient<ISpaceTradersApiClient>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<SpaceTradersOptions>>().Value;
                return new RefitSettings
                {
                    AuthorizationHeaderValueGetter = (_, ct) => options.ApiKeyProvider!(provider, ct),
                    ExceptionFactory = async response =>
                    {
                        if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NotFound) { return null; }

                        var error = await response.Content.ReadFromJsonAsync<SpaceTradersErrorResponse>();
                        throw new SpaceTradersApiException(
                            error!.Error.Message,
                            error.Error.Code,
                            response.StatusCode
                        );
                    }
                };
            })
            .AddHttpMessageHandler<RateLimitingHandler>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<SpaceTradersOptions>>().Value;
                client.BaseAddress = options.ApiBaseUri;
            });

        return services;
    }
}