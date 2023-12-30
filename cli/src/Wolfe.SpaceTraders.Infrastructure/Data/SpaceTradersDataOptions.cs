namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal class SpaceTradersDataOptions
{
    public required string RootDirectory { get; set; }

    public string MarketDataDirectory => Path.Combine(RootDirectory, "market-data");
}