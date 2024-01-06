namespace Wolfe.SpaceTraders.Domain.Ships;

public interface IShipCargoBase
{
    int Capacity { get; }
    IReadOnlyCollection<ShipCargoItem> Items { get; }
}

public interface IShipCargo : IShipCargoBase
{
    int Quantity { get; }
    decimal PercentRemaining { get; }
}