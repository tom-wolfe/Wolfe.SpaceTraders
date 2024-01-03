using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Agents.Commands;
using Wolfe.SpaceTraders.Domain.Agents.Results;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Factions;
using Wolfe.SpaceTraders.Domain.General;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Agents;
using Wolfe.SpaceTraders.Sdk.Requests;

namespace Wolfe.SpaceTraders.Infrastructure.Api;

internal static class Agents
{
    public static Agent ToDomain(this SpaceTradersAgent agent) => new()
    {
        Id = new AgentId(agent.Symbol),
        AccountId = new AccountId(agent.AccountId),
        Credits = new Credits(agent.Credits),
        Headquarters = new WaypointId(agent.Headquarters),
        FactionId = new FactionId(agent.StartingFaction)
    };

    public static SpaceTradersRegisterRequest ToApi(this CreateAgentCommand command) => new()
    {
        Symbol = command.Agent.Value,
        Faction = command.Faction.Value,
        Email = command.Email
    };

    public static CreateAgentResult ToDomain(
        this SpaceTradersRegistration response,
        IShipClient shipClient,
        IContractService contractService
    ) => new()
    {
        Agent = response.Agent.ToDomain(),
        Contract = response.Contract.ToDomain(contractService),
        Faction = response.Faction.ToDomain(),
        Ship = response.Ship.ToDomain(new AgentId(response.Agent.Symbol), shipClient),
        Token = response.Token
    };
}