namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal class MarketServiceOptions
{
    public required TimeSpan MinAge { get; init; }
    public required TimeSpan MaxAge { get; init; }
}