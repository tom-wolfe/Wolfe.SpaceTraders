using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Cli.Missions;

internal class LoggerMissionLog(ILogger<LoggerMissionLog> logger) : IMissionLog
{
    public ValueTask Write(FormattableString message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(message.Format, message.GetArguments());
        return ValueTask.CompletedTask;
    }
}
