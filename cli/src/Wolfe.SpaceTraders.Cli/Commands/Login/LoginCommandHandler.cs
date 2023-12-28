using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Login;

internal class LoginCommandHandler(IAgentService agentService, ITokenService tokenService) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var token = context.BindingContext.ParseResult.GetValueForArgument(LoginCommand.TokenArgument);
        await tokenService.SetAccessToken(token, context.GetCancellationToken());

        var agent = await agentService.GetAgent(context.GetCancellationToken());
        Console.WriteLine($"Welcome, {agent.Id}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}