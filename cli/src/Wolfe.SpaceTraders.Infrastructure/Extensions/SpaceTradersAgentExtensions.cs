using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Agents;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersAgentExtensions
{
    public static Agent ToDomain(this SpaceTradersAgent agent) => new()
    {
        Symbol = new AgentSymbol(agent.Symbol),
        AccountId = new AccountId(agent.AccountId),
        Credits = new Credits(agent.Credits),
        Headquarters = new WaypointSymbol(agent.Headquarters),
        StartingFaction = new FactionSymbol(agent.StartingFaction)
    };
}