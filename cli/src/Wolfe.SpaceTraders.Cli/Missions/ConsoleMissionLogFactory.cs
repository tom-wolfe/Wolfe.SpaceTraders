using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Service.Missions;

namespace Wolfe.SpaceTraders.Cli.Missions;

internal class ConsoleMissionLogFactory : IMissionLogFactory
{
    public IMissionLog CreateMissionLog() => new ConsoleMissionLog();
}
