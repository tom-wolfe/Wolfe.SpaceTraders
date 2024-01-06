namespace Wolfe.SpaceTraders.Domain.Ships;

internal record ShipFuel : IShipFuel
{
    public ShipFuel(IShipFuelBase fuel)
    {
        Current = fuel.Current;
        Capacity = fuel.Capacity;
    }

    public virtual Fuel Current { get; set; }
    public virtual Fuel Capacity { get; set; }

    public bool IsEmpty => Current == Fuel.Zero;
    public decimal PercentRemaining => Capacity == Fuel.Zero ? 0 : Current / Capacity * 100m;
}