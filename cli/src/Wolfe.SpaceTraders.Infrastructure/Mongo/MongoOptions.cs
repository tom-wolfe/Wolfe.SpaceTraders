namespace Wolfe.SpaceTraders.Infrastructure.Mongo;

internal class MongoOptions
{
    public required string ConnectionString { get; init; }
    public required string Database { get; init; }
    public required string WaypointsCollection { get; init; } = "waypoints";
}
