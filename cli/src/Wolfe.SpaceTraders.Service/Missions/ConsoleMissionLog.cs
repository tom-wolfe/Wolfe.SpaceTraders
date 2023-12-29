using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Service.Missions;

internal class ConsoleMissionLog : IMissionLog
{
    public void Log(string message)
    {
        // TODO: Add formatting, dates, etc.
        // TODO: Also add a file mission log.
        Console.WriteLine(message);
    }
}
