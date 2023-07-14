﻿namespace Wolfe.SpaceTraders.Models;

public class ContractTerms
{
    public DateTimeOffset Deadline { get; set; }
    public ContractPaymentTerms Payment { get; set; } = new();
    public List<ContractDeliveryTerm> Deliver { get; set; } = new();
}