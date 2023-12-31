using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Cli.Missions;

internal class ConsoleMissionLog : IMissionLog
{
    public void Write(FormattableString message)
    {
        ConsoleHelpers.WriteFormatted($"{DateTimeOffset.UtcNow:u}: ");
        ConsoleHelpers.WriteLineFormatted(message);
    }
}
