namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public partial struct SystemId
{
    public SectorId Sector => new(Value[..Value.IndexOf('-')]);
}