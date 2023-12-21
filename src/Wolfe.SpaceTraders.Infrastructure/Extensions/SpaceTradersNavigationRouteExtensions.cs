﻿using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Sdk.Models.Navigation;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersNavigationRouteExtensions
{
    public static NavigationRoute ToDomain(this SpaceTradersNavigationRoute route) => new()
    {
        Arrival = route.Arrival,
        Departure = route.Departure.ToDomain(),
        Destination = route.Destination.ToDomain(),
        DepartureTime = route.DepartureTime,
    };
}