﻿using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Domain.Contracts;

/// <summary>
/// Defines the payment terms for a contract.
/// </summary>
public class ContractPaymentTerms
{
    /// <summary>
    /// The amount of credits received up front for accepting the contract.
    /// </summary>
    public Credits OnAccepted { get; init; } = Credits.Zero;

    /// <summary>
    /// The amount of credits received when the contract is fulfilled.
    /// </summary>
    public Credits OnFulfilled { get; init; } = Credits.Zero;
}