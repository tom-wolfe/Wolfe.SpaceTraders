﻿using Humanizer;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ShipFormatter
{
    public static void WriteShip(Ship ship)
    {
        Console.WriteLine($"~ {ship.Symbol.Value.Color(ConsoleColors.Id)} ({ship.Registration.Role.Value.Color(ConsoleColors.Category)}) [{ship.Navigation.Status.Value.Color(ConsoleColors.Status)}]");
        Console.WriteLine($"  Point: {ship.Navigation.WaypointSymbol.Value.Color(ConsoleColors.Code)}");

        if (ship.Navigation.Status == NavigationStatus.InTransit)
        {
            Console.WriteLine($"  Arrival: {ship.Navigation.Route.Arrival.Humanize().Color(ConsoleColors.Information)}");
        }

        if (!ship.Fuel.IsEmpty)
        {
            var fuel = $"{ship.Fuel.Current}/{ship.Fuel.Capacity} ({ship.Fuel.PercentRemaining}%)".Color(ConsoleColors.Fuel);
            Console.WriteLine($"  Fuel: {fuel}");
        }

        if (ship.Cargo.Capacity > 0)
        {
            Console.WriteLine($"  Cargo: {ship.Cargo.Units}/{ship.Cargo.Capacity} ({ship.Cargo.PercentRemaining}%)");
            foreach (var item in ship.Cargo.Inventory)
            {
                Console.WriteLine($"  - {item.Units} {item.Name.Color(ConsoleColors.Information)} ({item.Symbol.Value.Color(ConsoleColors.Code)})");
            }
        }
    }
}