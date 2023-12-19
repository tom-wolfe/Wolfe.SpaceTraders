﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System.Net.Http.Json;
using Wolfe.SpaceTraders.Sdk.Responses;

namespace Wolfe.SpaceTraders.Sdk;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSpaceTradersApi(this IServiceCollection services, Action<SpaceTradersOptions> configure)
    {
        services
            .AddOptions<SpaceTradersOptions>()
            .Configure(configure)
            .Validate(o => o.ApiKeyProvider != null, $"{nameof(SpaceTradersOptions.ApiKeyProvider)} cannot be null.");

        // TODO: Investigate throttling.
        services
            .AddRefitClient<ISpaceTradersApiClient>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<SpaceTradersOptions>>().Value;
                return new RefitSettings
                {
                    AuthorizationHeaderValueGetter = options.ApiKeyProvider,
                    ExceptionFactory = async response =>
                    {
                        if (response.IsSuccessStatusCode) { return null; }

                        var error = await response.Content.ReadFromJsonAsync<SpaceTradersErrorResponse>();
                        return new SpaceTradersApiException(
                            error!.Error.Message,
                            error.Error.Code,
                            response.StatusCode
                        );
                    }
                };
            })
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<SpaceTradersOptions>>().Value;
                client.BaseAddress = options.ApiBaseUri;
            });

        return services;
    }
}