using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Service.Missions;

internal class ConsoleMissionLog : IMissionLog
{
    public void Log(string message)
    {
        Console.WriteLine($"{DateTimeOffset.UtcNow}: {message}");
    }
}
