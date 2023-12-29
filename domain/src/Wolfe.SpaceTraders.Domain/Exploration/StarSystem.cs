using Wolfe.SpaceTraders.Domain.Navigation;

namespace Wolfe.SpaceTraders.Domain.Exploration;

public class StarSystem
{
    public required SystemId Id { get; init; }
    public SectorId SectorId => Id.Sector;
    public required SystemType Type { get; init; }
    public required Point Location { get; init; }
}
