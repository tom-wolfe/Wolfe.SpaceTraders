﻿using Microsoft.Extensions.Logging;
using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class LoggerMissionLog(ILogger<LoggerMissionLog> logger) : IMissionLog
{
    public ValueTask Write(FormattableString message, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(message.Format, message.GetArguments());
        return ValueTask.CompletedTask;
    }

    public ValueTask WriteError(Exception ex, CancellationToken cancellationToken = default)
    {
        logger.LogError(ex, "Unhandled error while executing mission.");
        return ValueTask.CompletedTask;
    }
}
