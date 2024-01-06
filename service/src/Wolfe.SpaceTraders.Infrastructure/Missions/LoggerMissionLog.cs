using Microsoft.Extensions.Logging;
using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class LoggerMissionLog(ILogger<LoggerMissionLog> logger) : IMissionLog
{
    public void OnStatusChanged(IMission mission, MissionStatus status) => OnEvent(mission, $"Mission status changed to: {status}.");

    public void OnEvent(IMission mission, FormattableString message) => logger.LogInformation(message.Format, message.GetArguments());

    public void OnError(IMission mission, Exception ex) => logger.LogError(ex, "Unhandled error while executing mission.");
}
