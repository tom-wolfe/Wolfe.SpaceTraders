namespace Wolfe.SpaceTraders.Domain.Contracts;

public class Contract
{
    public required ContractId Id { get; init; }
    public required FactionId FactionId { get; init; }
    public required ContractType Type { get; init; }
    public required ContractTerms Terms { get; init; }
    public bool Fulfilled { get; init; }
    public bool Accepted { get; init; }
    public DateTimeOffset DeadlineToAccept { get; init; }

    public IEnumerable<ContractItem> GetOutstandingItems() =>
        Terms.Items.Where(d => d.QuantityRemaining > 0);

    public bool IsComplete() => Accepted && !GetOutstandingItems().Any();
}