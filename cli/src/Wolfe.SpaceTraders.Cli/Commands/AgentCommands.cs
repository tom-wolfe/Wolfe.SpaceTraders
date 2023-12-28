using Cocona;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Service;
using Wolfe.SpaceTraders.Service.Commands;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class AgentCommands(IAgentService agentService, ITokenService tokenService)
{
    public async Task<int> Login([Argument] string token, CancellationToken cancellationToken = default)
    {
        await tokenService.SetAccessToken(token, cancellationToken);

        var agent = await agentService.GetAgent(cancellationToken);
        Console.WriteLine($"Welcome, {agent.Id}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Me(CancellationToken cancellationToken = default)
    {
        var agent = await agentService.GetAgent(cancellationToken);
        AgentFormatter.WriteAgent(agent);
        return ExitCodes.Success;
    }

    public async Task<int> Register([Argument] AgentId name, [Option] FactionId? faction, [Option] string? email, CancellationToken cancellationToken = default)
    {
        var request = new CreateAgentCommand
        {
            Agent = name,
            Faction = faction ?? FactionId.Cosmic, // Default faction.
            Email = email
        };
        var response = await agentService.CreateAgent(request, cancellationToken);
        await tokenService.SetAccessToken(response.Token, cancellationToken);

        Console.WriteLine($"Welcome, {response.Agent.Id}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}
