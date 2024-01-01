using Microsoft.Extensions.Logging;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Missions.Logs;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class LoggerMissionLogProvider(ILoggerFactory loggerFactory) : IMissionLogProvider
{
    public IMissionLog CreateLog(MissionId missionId) => new LoggerMissionLog(loggerFactory.CreateLogger<LoggerMissionLog>());
}
