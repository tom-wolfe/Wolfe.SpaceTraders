﻿namespace Wolfe.SpaceTraders.Domain.Models;

public class ShipFuel
{
    public required Fuel Current { get; set; }
    public required Fuel Capacity { get; set; }
    public ShipFuelConsumed? Consumed { get; set; }

    public bool IsEmpty => Current == Fuel.Zero;
    public decimal PercentRemaining => Current / Capacity * 100m;

}