﻿using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersContractPaymentTermsExtensions
{
    public static ContractPaymentTerms ToDomain(this SpaceTradersContractPaymentTerms terms) => new()
    {
        OnAccepted = new Credits(terms.OnAccepted),
        OnFulfilled = new Credits(terms.OnFulfilled),
    };
}