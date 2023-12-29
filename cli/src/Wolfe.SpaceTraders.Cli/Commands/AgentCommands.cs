using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Factions;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Service.Agents;
using Wolfe.SpaceTraders.Service.Agents.Commands;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class AgentCommands(IAgentService agentService, ITokenService tokenService, IHostApplicationLifetime host)
{
    public async Task<int> Login([Argument] string token)
    {
        await tokenService.SetAccessToken(token, host.ApplicationStopping);

        var agent = await agentService.GetAgent(host.ApplicationStopping);
        Console.WriteLine($"Welcome, {agent.Id}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    [PrimaryCommand]
    public async Task<int> Me()
    {
        var agent = await agentService.GetAgent(host.ApplicationStopping);
        AgentFormatter.WriteAgent(agent);
        return ExitCodes.Success;
    }

    public async Task<int> Register([Argument] AgentId name, [Option] FactionId? faction, [Option] string? email)
    {
        var request = new CreateAgentCommand
        {
            Agent = name,
            Faction = faction ?? FactionId.Cosmic, // Default faction.
            Email = email
        };
        var response = await agentService.CreateAgent(request, host.ApplicationStopping);
        await tokenService.SetAccessToken(response.Token, host.ApplicationStopping);

        Console.WriteLine($"Welcome, {response.Agent.Id}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}
