namespace Wolfe.SpaceTraders.Domain.Ships;

public interface IShipFuelBase
{
    public Fuel Current { get; }
    public Fuel Capacity { get; }
}

public interface IShipFuel : IShipFuelBase
{
    public bool IsEmpty { get; }
    public decimal PercentRemaining { get; }
}