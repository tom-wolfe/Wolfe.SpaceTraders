using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Service.Missions;

internal class ConsoleMissionLogFactory : IMissionLogFactory
{
    public IMissionLog CreateMissionLog() => new ConsoleMissionLog();
}
