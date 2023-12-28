namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataShipyardTransaction
{
    public required string WaypointId { get; init; }
    public required string ShipId { get; init; }
    public required long Price { get; init; }
    public required string AgentId { get; init; }
    public required DateTimeOffset Timestamp { get; init; }
}