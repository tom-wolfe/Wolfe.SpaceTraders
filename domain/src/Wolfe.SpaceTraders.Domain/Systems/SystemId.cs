namespace Wolfe.SpaceTraders.Domain.Systems;

[StronglyTypedId]
public partial struct SystemId
{
    public SectorId Sector => new(Value[..Value.IndexOf('-')]);
}