﻿namespace Wolfe.SpaceTraders.Domain.Ships;

public class ShipCooldown
{
    public required TimeSpan Total { get; set; }
    public TimeSpan Remaining => Expiration - DateTimeOffset.UtcNow;
    public required DateTimeOffset Expiration { get; set; }
}