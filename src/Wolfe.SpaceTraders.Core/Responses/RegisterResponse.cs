﻿using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Core.Responses;

public class RegisterResponse
{
    public required Agent Agent { get; set; }
    public required Contract Contract { get; set; }
    public required Faction Faction { get; set; }
    public required Ship Ship { get; set; }
    public required string Token { get; set; }
}