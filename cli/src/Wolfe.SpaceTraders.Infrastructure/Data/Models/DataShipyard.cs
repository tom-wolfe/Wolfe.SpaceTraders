namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataShipyard : DataWaypoint
{
    public required List<DataShipyardShipType> ShipTypes { get; init; } = [];
    public List<DataShipyardTransaction> Transactions { get; init; } = [];
    public List<DataShipyardShip> Ships { get; init; } = [];
}