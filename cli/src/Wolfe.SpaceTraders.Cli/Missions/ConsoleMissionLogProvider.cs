using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Service.Missions;

namespace Wolfe.SpaceTraders.Cli.Missions;

internal class ConsoleMissionLogProvider : IMissionLogProvider
{
    public IMissionLog CreateLog(MissionId missionId) => new ConsoleMissionLog();
}
