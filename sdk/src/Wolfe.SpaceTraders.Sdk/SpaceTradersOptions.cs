using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Wolfe.SpaceTraders.Sdk;

public class SpaceTradersOptions
{
    /// <summary>
    /// Gets the base URI for the SpaceTraders API.
    /// </summary>
    public required Uri ApiBaseUri { get; init; }

    /// <summary>
    /// Gets the rate limits for the SpaceTraders API.
    /// </summary>
    public required SpaceTradersRateLimits RateLimits { get; init; }

    /// <summary>
    /// Gets the name of the environment variable that contains the API key for the SpaceTraders API.
    /// </summary>
    public string? ApiKeyEnvironmentVariable { get; init; } = "SPACETRADERS_API_KEY";

    /// <summary>
    /// Gets a function that provides the API key for the SpaceTraders API.
    /// By default, this will read the API key from the <see cref="ApiKeyEnvironmentVariable"/> environment variable.
    /// </summary>
    public required Func<IServiceProvider, CancellationToken, ValueTask<string>> ApiKeyProvider { get; set; } =
        GetApiKeyFromEnvironmentVariable;

    private static ValueTask<string> GetApiKeyFromEnvironmentVariable(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var options = serviceProvider.GetRequiredService<IOptions<SpaceTradersOptions>>().Value;
        if (string.IsNullOrEmpty(options.ApiKeyEnvironmentVariable))
        {
            throw new InvalidOperationException($"{nameof(options.ApiKeyEnvironmentVariable)} is missing.");
        }
        var apiKey = Environment.GetEnvironmentVariable(options.ApiKeyEnvironmentVariable);
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException($"{options.ApiKeyEnvironmentVariable} is missing.");
        }
        return ValueTask.FromResult(apiKey);
    }
}