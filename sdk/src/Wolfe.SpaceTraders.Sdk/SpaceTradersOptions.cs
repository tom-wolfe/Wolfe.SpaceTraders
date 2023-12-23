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
    /// Gets a function that provides the API key for the SpaceTraders API.
    /// </summary>
    public required Func<IServiceProvider, CancellationToken, Task<string>>? ApiKeyProvider { get; set; }
}