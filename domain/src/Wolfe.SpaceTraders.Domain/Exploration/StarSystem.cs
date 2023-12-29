using Wolfe.SpaceTraders.Domain.Navigation;

namespace Wolfe.SpaceTraders.Domain.Exploration;

public class StarSystem
{
    /// <summary>
    /// The ID of the system.
    /// </summary>
    public required SystemId Id { get; init; }

    /// <summary>
    /// The ID of the sector the system is in.
    /// </summary>
    public SectorId SectorId => Id.Sector;

    /// <summary>
    /// The type of system.
    /// </summary>
    public required SystemType Type { get; init; }

    /// <summary>
    /// Relative location of the system in the sector.
    /// </summary>
    public required Point Location { get; init; }
}
