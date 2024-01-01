using Wolfe.SpaceTraders.Infrastructure.Exploration.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Shipyards.Models;

internal class MongoShipyard : MongoWaypoint
{
    public required IReadOnlyCollection<MongoShipyardShipType> ShipTypes { get; init; } = [];
}

internal record MongoShipyardShipType(string Type);