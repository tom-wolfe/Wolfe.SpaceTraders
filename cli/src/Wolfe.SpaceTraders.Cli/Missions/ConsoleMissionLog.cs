using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Cli.Missions;

internal class ConsoleMissionLog : IMissionLog
{
    public ValueTask Write(FormattableString message, CancellationToken cancellationToken = default)
    {
        ConsoleHelpers.WriteFormatted($"{DateTimeOffset.UtcNow:u}: ");
        ConsoleHelpers.WriteLineFormatted(message);
        return ValueTask.CompletedTask;
    }
}
