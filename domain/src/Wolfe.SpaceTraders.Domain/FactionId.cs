namespace Wolfe.SpaceTraders.Domain;

[StronglyTypedId]
public partial struct FactionId
{
    public static FactionId Cosmic => new("COSMIC");
}