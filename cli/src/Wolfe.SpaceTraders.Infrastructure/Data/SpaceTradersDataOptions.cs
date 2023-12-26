namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal class SpaceTradersDataOptions
{
    public required string RootDirectory { get; set; }
    public required string WaypointsDirectory { get; set; } = "waypoints";
    public required string SystemsDirectory { get; set; } = "systems";
    public required string ShipyardsDirectory { get; set; } = "shipyards";
    public required string MarketplacesDirectory { get; set; } = "marketplaces";

    public string WaypointsDirectoryPath => Path.Combine(RootDirectory, WaypointsDirectory);
    public string SystemsDirectoryPath => Path.Combine(RootDirectory, SystemsDirectoryPath);
    public string ShipyardsDirectoryPath => Path.Combine(RootDirectory, ShipyardsDirectory);
    public string MarketplacesDirectoryPath => Path.Combine(RootDirectory, MarketplacesDirectory);
}