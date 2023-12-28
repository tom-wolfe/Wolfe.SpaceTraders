using Wolfe.SpaceTraders.Domain.Systems;

namespace Wolfe.SpaceTraders.Domain.Waypoints;

[StronglyTypedId]
public partial struct WaypointId
{
    private string SectorString => Value.Split('-')[0];
    private string SystemString => Value.Split('-')[1];
    public SystemId System => new($"{SectorString}-{SystemString}");
}