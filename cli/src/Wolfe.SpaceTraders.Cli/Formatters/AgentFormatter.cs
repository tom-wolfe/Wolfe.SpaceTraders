using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Agents;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class AgentFormatter
{
    public static void WriteAgent(Agent agent)
    {
        Console.WriteLine($"~ {agent.Id.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"  Account Id: {agent.AccountId.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"  Headquarters: {agent.Headquarters.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"  Credits: {agent.Credits.ToString().Color(ConsoleColors.Currency)}");
        Console.WriteLine($"  Faction: {agent.FactionId.Value.Color(ConsoleColors.Code)}");
    }
}