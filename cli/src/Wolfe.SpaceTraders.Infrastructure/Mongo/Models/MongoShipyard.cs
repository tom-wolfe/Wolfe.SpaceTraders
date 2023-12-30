namespace Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

internal class MongoShipyard : MongoWaypoint
{
    public required IReadOnlyCollection<MongoShipyardShipType> ShipTypes { get; init; } = [];
}

internal record MongoShipyardShipType(string Type);