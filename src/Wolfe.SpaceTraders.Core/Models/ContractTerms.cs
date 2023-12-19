﻿namespace Wolfe.SpaceTraders.Core.Models;

public class ContractTerms
{
    public required DateTimeOffset Deadline { get; set; }
    public required ContractPaymentTerms Payment { get; set; } = new();
    public required IReadOnlyCollection<ContractGood> Deliver { get; set; }
}