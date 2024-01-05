namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// Defines a type of mission.
/// </summary>
[StronglyTypedId]
public partial struct MissionStatus
{
    public static readonly MissionStatus New = new("NEW");
    public static readonly MissionStatus Running = new("RUNNING");
    public static readonly MissionStatus Stopping = new("STOPPING");
    public static readonly MissionStatus Suspended = new("SUSPENDED");
    public static readonly MissionStatus Error = new("ERROR");
    public static readonly MissionStatus Complete = new("COMPLETE");
}