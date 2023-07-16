namespace Wolfe.SpaceTraders.Core.Models;

public class ShipNavRoute
{
    public required ShipNavLocation Destination { get; set; }
    public required ShipNavLocation Departure { get; set; }
    public required DateTimeOffset DepartureTime { get; set; }
    public required DateTimeOffset Arrival { get; set; }
}