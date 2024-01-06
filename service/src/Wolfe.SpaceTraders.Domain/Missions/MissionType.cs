namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// Defines a type of mission.
/// </summary>
[StronglyTypedId]
public partial struct MissionType
{
    public static readonly MissionType Probe = new("PROBE");
    public static readonly MissionType Trading = new("TRADING");
}