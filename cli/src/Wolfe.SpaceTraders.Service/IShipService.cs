﻿using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service;

public interface IShipService
{
    public Task<Ship?> GetShip(ShipId shipId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Ship> GetShips(CancellationToken cancellationToken = default);
}