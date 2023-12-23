namespace Wolfe.SpaceTraders.Domain;

[StronglyTypedId]
public partial struct FactionSymbol
{
    public static FactionSymbol Cosmic => new("COSMIC");
}