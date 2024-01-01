namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// Unique identifier of a mission.
/// </summary>
[StronglyTypedId]
public partial struct MissionId
{
    /// <summary>
    /// Generates a new <see cref="MissionId"/>.
    /// </summary>
    /// <returns>The generated mission ID.</returns>
    public static MissionId Generate() => new(Guid.NewGuid().ToString());
}