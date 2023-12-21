﻿using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Service.Responses;

public class AcceptContractResponse
{
    public required Agent Agent { get; set; }
    public required Contract Contract { get; set; }
}