namespace Wolfe.SpaceTraders.Domain.Exploration;

[StronglyTypedId]
public readonly partial struct WaypointId
{
    /// <summary>
    /// Gets the system id of the waypoint.
    /// </summary>
    public SystemId SystemId => new(Value[..Value.LastIndexOf('-')]);
}