namespace Wolfe.SpaceTraders.Sdk;

/// <summary>
/// Defines the rate limiting options for the Space Traders API.
/// </summary>
public class SpaceTradersRateLimits
{
    /// <summary>
    /// Gets or sets the maximum number of requests that can be backlogged before an exception is thrown.
    /// </summary>
    public int MaxQueueLength { get; init; }

    /// <summary>
    /// Gets the number of requests allowed during the <see cref="Interval"/>.
    /// </summary>
    public int RequestsPerInterval { get; init; }

    /// <summary>
    /// Gets the length of the sliding window during which requests are counted.
    /// </summary>
    public TimeSpan Interval { get; init; }
}