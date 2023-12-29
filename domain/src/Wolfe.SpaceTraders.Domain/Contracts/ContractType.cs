namespace Wolfe.SpaceTraders.Domain.Contracts;

[StronglyTypedId]
public partial struct ContractType
{
    /// <summary>
    /// Specifies that the contract is to procure goods and deliver them to a location.
    /// </summary>
    public static readonly ContractType Procurement = new("PROCUREMENT");
    public static readonly ContractType Transport = new("TRANSPORT");
    public static readonly ContractType Shuttle = new("SHUTTLE");
}