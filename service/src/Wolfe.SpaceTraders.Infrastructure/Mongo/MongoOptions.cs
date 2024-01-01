namespace Wolfe.SpaceTraders.Infrastructure.Mongo;

internal class MongoOptions
{
    public required string ConnectionString { get; init; }
    public required string Database { get; init; }

    public required string MarketDataCollection { get; init; } = "market-data";
    public required string MarketplacesCollection { get; init; } = "marketplaces";
    public required string MissionLogsCollection { get; init; } = "mission-logs";
    public required string ShipyardsCollection { get; init; } = "shipyards";
    public required string SystemsCollection { get; init; } = "systems";
    public required string TokensCollection { get; init; } = "tokens";
    public required string WaypointsCollection { get; init; } = "waypoints";
}
