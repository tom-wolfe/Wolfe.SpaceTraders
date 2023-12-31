using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ShipFormatter
{
    public static void WriteShip(Ship ship)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {ship.Id} ({ship.Role}) [[{ship.Navigation.Status}]]");
        ConsoleHelpers.WriteLineFormatted($"  Location: {ship.Navigation.WaypointId}");
        if (ship.Navigation.Status == ShipNavigationStatus.InTransit)
        {
            ConsoleHelpers.WriteLineFormatted($"  Arrival: {ship.Navigation.Route.Arrival}");
        }

        if (!ship.Fuel.IsEmpty)
        {
            ConsoleHelpers.WriteLineFormatted($"  Fuel: {ship.Fuel.Current}/{ship.Fuel.Capacity} ({ship.Fuel.PercentRemaining}%)");
        }

        if (ship.Cargo.Capacity > 0)
        {
            ConsoleHelpers.WriteLineFormatted($"  Cargo: {ship.Cargo.Quantity}/{ship.Cargo.Capacity} ({ship.Cargo.PercentRemaining}%)");
            foreach (var item in ship.Cargo.Items)
            {
                ConsoleHelpers.WriteLineFormatted($"  - {item.Quantity} * ({item.Id}) {item.Name}");
            }
        }
    }
}