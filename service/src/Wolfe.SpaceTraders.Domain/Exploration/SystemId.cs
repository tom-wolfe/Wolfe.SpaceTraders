namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public readonly partial struct SystemId
{
    public SectorId Sector => new(Value[..Value.IndexOf('-')]);
}