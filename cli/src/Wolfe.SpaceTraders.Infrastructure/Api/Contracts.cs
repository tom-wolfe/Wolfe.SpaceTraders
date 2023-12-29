using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Contracts.Results;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Factions;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class Contracts
{
    public static Contract ToDomain(this SpaceTradersContract contract, IContractClient client) => new(client, contract.Accepted)
    {
        Id = new ContractId(contract.Id),
        FactionId = new FactionId(contract.FactionSymbol),
        Type = new ContractType(contract.Type),
        Terms = contract.Terms.ToDomain(),
        Fulfilled = contract.Fulfilled,
        DeadlineToAccept = contract.DeadlineToAccept,
    };

    public static ContractItem ToDomain(this SpaceTradersContractGood good) => new()
    {
        DestinationId = new WaypointId(good.DestinationSymbol),
        ItemId = new ItemId(good.TradeSymbol),
        QuantityFulfilled = good.UnitsFulfilled,
        QuantityRequired = good.UnitsRequired,
    };

    public static ContractPaymentTerms ToDomain(this SpaceTradersContractPaymentTerms terms) => new()
    {
        OnAccepted = new Credits(terms.OnAccepted),
        OnFulfilled = new Credits(terms.OnFulfilled),
    };

    public static ContractTerms ToDomain(this SpaceTradersContractTerms terms) => new()
    {
        Deadline = terms.Deadline,
        Payment = terms.Payment.ToDomain(),
        Items = terms.Deliver.Select(d => d.ToDomain()).ToList(),
    };

    public static AcceptContractResult ToDomain(this SpaceTradersAcceptedContract contract, IContractClient client) => new()
    {
        Agent = contract.Agent.ToDomain(),
        Contract = contract.Contract.ToDomain(client),
    };
}
