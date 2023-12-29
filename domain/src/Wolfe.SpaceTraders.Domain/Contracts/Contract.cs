namespace Wolfe.SpaceTraders.Domain.Contracts;

public class Contract(IContractClient client, bool accepted)
{
    /// <summary>
    /// ID of the contract.
    /// </summary>
    public required ContractId Id { get; init; }

    /// <summary>
    /// The ID of the faction that this contract is for.
    /// </summary>
    public required Factions.FactionId FactionId { get; init; }

    /// <summary>
    /// Type of contract.
    /// </summary>
    public required ContractType Type { get; init; }

    /// <summary>
    /// The terms to fulfill the contract.
    /// </summary>
    public required ContractTerms Terms { get; init; }

    /// <summary>
    /// Whether the contract has been fulfilled
    /// </summary>
    public bool Fulfilled { get; init; }

    /// <summary>
    /// Whether the contract has been accepted by the agent.
    /// </summary>
    public bool Accepted { get; private set; } = accepted;

    /// <summary>
    /// The time at which the contract is no longer available to be accepted.
    /// </summary>
    public DateTimeOffset DeadlineToAccept { get; init; }

    /// <summary>
    /// The time at which the contract is no longer available to be completed.
    /// </summary>
    public DateTimeOffset DeadlineToComplete => Terms.Deadline;

    /// <summary>
    /// Gets the amount of time remaining to accept the contract.
    /// </summary>
    public TimeSpan TimeToAccept => DeadlineToAccept - DateTimeOffset.UtcNow;

    /// <summary>
    /// Gets the amount of time remaining to complete the contract.
    /// </summary>
    public TimeSpan TimeToComplete => DeadlineToComplete - DateTimeOffset.UtcNow;

    /// <summary>
    /// On a delivery contract, gets the items that still need to be delivered.
    /// </summary>
    public IEnumerable<ContractItem> GetOutstandingItems() =>
        Terms.Items.Where(d => d.QuantityRemaining > 0);

    public bool IsComplete() => Accepted && !GetOutstandingItems().Any();

    /// <summary>
    /// Accepts the contract on behalf of the agent.
    /// </summary>
    /// <remarks>
    /// You can only accept contracts that were offered to you, were not accepted yet, and whose deadlines has not passed yet.
    /// </remarks>
    public async Task Accept(CancellationToken cancellationToken = default)
    {
        await client.AcceptContract(Id, cancellationToken);
        Accepted = true;
    }
}