using Microsoft.Extensions.Logging;
using Wolfe.SpaceTraders.Domain.Missions.Logs;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class LoggerMissionLog(ILogger<LoggerMissionLog> logger) : IMissionLog
{
    public ValueTask Write(FormattableString message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(message.Format, message.GetArguments());
        return ValueTask.CompletedTask;
    }
}
