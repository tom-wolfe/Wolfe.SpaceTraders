namespace Wolfe.SpaceTraders.Domain.Models;

[StronglyTypedId]
public partial struct FactionSymbol
{
    public static FactionSymbol Cosmic => new("COSMIC");
}