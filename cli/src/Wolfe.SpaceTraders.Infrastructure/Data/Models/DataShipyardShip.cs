namespace Wolfe.SpaceTraders.Infrastructure.Data.Models;

internal class DataShipyardShip
{
    public required string Type { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required long PurchasePrice { get; init; }
}