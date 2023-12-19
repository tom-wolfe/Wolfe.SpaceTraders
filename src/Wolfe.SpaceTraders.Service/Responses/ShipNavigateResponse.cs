﻿using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Service.Responses;

public class ShipNavigateResponse
{
    public required ShipFuel Fuel { get; set; }
    public required Navigation Navigation { get; set; }
}