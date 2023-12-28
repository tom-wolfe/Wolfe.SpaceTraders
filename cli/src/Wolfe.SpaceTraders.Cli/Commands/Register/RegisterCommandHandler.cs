using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Service;
using Wolfe.SpaceTraders.Service.Commands;

namespace Wolfe.SpaceTraders.Cli.Commands.Register;

internal class RegisterCommandHandler(IAgentService agentService, ITokenService token) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var agentId = context.BindingContext.ParseResult.GetValueForArgument(RegisterCommand.AgentIdArgument);
        var faction = context.BindingContext.ParseResult.GetValueForOption(RegisterCommand.FactionOption);
        var email = context.BindingContext.ParseResult.GetValueForOption(RegisterCommand.EmailOption);

        var request = new CreateAgentCommand
        {
            Agent = agentId,
            Faction = faction ?? FactionId.Cosmic, // Default faction.
            Email = email
        };
        var response = await agentService.CreateAgent(request, context.GetCancellationToken());
        await token.SetAccessToken(response.Token, context.GetCancellationToken());

        Console.WriteLine($"Welcome, {response.Agent.Id}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}