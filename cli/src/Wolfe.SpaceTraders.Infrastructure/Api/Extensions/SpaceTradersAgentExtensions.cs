using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Factions;
using Wolfe.SpaceTraders.Sdk.Models.Agents;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersAgentExtensions
{
    public static Agent ToDomain(this SpaceTradersAgent agent) => new()
    {
        Id = new AgentId(agent.Symbol),
        AccountId = new AccountId(agent.AccountId),
        Credits = new Credits(agent.Credits),
        Headquarters = new WaypointId(agent.Headquarters),
        FactionId = new FactionId(agent.StartingFaction)
    };
}