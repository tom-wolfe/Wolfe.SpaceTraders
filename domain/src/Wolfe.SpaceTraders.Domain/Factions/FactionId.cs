namespace Wolfe.SpaceTraders.Domain.Factions;

[StronglyTypedId]
public partial struct FactionId
{
    public static FactionId Cosmic => new("COSMIC");
}