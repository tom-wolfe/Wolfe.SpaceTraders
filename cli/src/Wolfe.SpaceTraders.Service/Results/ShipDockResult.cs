﻿using Wolfe.SpaceTraders.Domain.Navigation;

namespace Wolfe.SpaceTraders.Service.Results;

public class ShipDockResult
{
    public required Navigation Navigation { get; set; }
}