﻿using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Service.Missions;

internal class ConsoleMissionLog : IMissionLog
{
    public void Write(string message)
    {
        Console.WriteLine($"{message}");
    }
}
