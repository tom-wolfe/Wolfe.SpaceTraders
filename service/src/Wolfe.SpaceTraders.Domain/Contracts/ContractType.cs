namespace Wolfe.SpaceTraders.Domain.Contracts;

/// <summary>
/// The type of contract.
/// </summary>
[StronglyTypedId]
public readonly partial struct ContractType
{
    /// <summary>
    /// Specifies that the contract is to procure goods and deliver them to a location.
    /// </summary>
    public static readonly ContractType Procurement = new("PROCUREMENT");

    /// <summary>
    /// ?
    /// </summary>
    public static readonly ContractType Transport = new("TRANSPORT");

    /// <summary>
    /// ?
    /// </summary>
    public static readonly ContractType Shuttle = new("SHUTTLE");
}