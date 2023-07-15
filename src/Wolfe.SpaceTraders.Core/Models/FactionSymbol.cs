namespace Wolfe.SpaceTraders.Core.Models;

[StronglyTypedId]
public partial struct FactionSymbol
{
    public static FactionSymbol Cosmic => new("COSMIC");
}