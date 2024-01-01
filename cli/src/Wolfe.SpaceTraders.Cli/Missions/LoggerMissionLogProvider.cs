using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Service.Missions;

namespace Wolfe.SpaceTraders.Cli.Missions;

internal class LoggerMissionLogProvider(ILoggerFactory loggerFactory) : IMissionLogProvider
{
    public IMissionLog CreateLog(MissionId missionId) => new LoggerMissionLog(loggerFactory.CreateLogger<LoggerMissionLog>());
}
