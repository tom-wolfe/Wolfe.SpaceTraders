namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal class SpaceTradersDataOptions
{
    public required string RootDirectory { get; set; }

    public string AccessTokensDirectory => Path.Combine(RootDirectory, "tokens");
    public string AgentsDirectory => Path.Combine(RootDirectory, "agents");
    public string MarketDataDirectory => Path.Combine(RootDirectory, "market-data");
    public string MarketplacesDirectory => Path.Combine(RootDirectory, "marketplaces");
    public string ShipyardsDirectory => Path.Combine(RootDirectory, "shipyards");
    public string SystemsDirectory => Path.Combine(RootDirectory, "systems");

}