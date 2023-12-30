using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Exploration.Models;
using Wolfe.SpaceTraders.Infrastructure.Shipyards.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Shipyards.Models;

internal static class Mapping
{
    public static MongoShipyard ToMongo(this Shipyard shipyard) => new()
    {
        Id = shipyard.Id.Value,
        SystemId = shipyard.Id.SystemId.Value,
        Type = shipyard.Type.Value,
        Location = shipyard.Location.ToMongo(),
        Traits = shipyard.Traits.Select(t => t.ToMongo()).ToList(),
        ShipTypes = shipyard.ShipTypes.Select(t => t.ToMongo()).ToList(),
    };

    public static Shipyard ToDomain(this MongoShipyard shipyard) => new()
    {
        Id = new WaypointId(shipyard.Id),
        Type = new WaypointType(shipyard.Type),
        Location = shipyard.Location.ToDomain(),
        Traits = shipyard.Traits.Select(t => t.ToDomain()).ToList(),
        ShipTypes = shipyard.ShipTypes.Select(t => t.ToDomain()).ToList(),
    };

    private static MongoShipyardShipType ToMongo(this ShipyardShipType type) => new(type.Type.Value);

    private static ShipyardShipType ToDomain(this MongoShipyardShipType type) => new()
    {
        Type = new ShipType(type.Type),
    };
}
