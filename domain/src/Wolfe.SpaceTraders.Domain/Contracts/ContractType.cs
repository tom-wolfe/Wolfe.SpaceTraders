﻿namespace Wolfe.SpaceTraders.Domain.Contracts;

[StronglyTypedId]
public partial struct ContractType
{
    public static readonly ContractType Procurement = new("PROCUREMENT");
    public static readonly ContractType Transport = new("TRANSPORT");
    public static readonly ContractType Shuttle = new("SHUTTLE");
}