﻿using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Contracts;

namespace Wolfe.SpaceTraders.Service.Results;

public class AcceptContractResult
{
    public required Agent Agent { get; init; }
    public required Contract Contract { get; init; }
}