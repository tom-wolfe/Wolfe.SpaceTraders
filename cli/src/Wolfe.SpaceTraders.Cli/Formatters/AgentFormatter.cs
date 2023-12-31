using Wolfe.SpaceTraders.Domain.Agents;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class AgentFormatter
{
    public static void WriteAgent(Agent agent)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {agent.Id}");
        ConsoleHelpers.WriteLineFormatted($"  Account ID: {agent.AccountId}");
        ConsoleHelpers.WriteLineFormatted($"  Faction: {agent.FactionId}");
        ConsoleHelpers.WriteLineFormatted($"  Headquarters: {agent.Headquarters}");
        ConsoleHelpers.WriteLineFormatted($"  Credits: {agent.Credits}");
    }
}