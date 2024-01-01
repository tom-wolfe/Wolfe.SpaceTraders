using System.Diagnostics;

namespace Wolfe.SpaceTraders.Domain.Marketplaces;

[StronglyTypedId]
[DebuggerDisplay("{Value}")]
public partial struct TransactionType
{
    public static readonly TransactionType Purchase = new("PURCHASE");
    public static readonly TransactionType Sell = new("SELL");
}