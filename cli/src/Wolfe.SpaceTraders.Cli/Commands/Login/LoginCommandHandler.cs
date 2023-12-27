using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Login;

internal class LoginCommandHandler(ITokenService token, ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var token1 = context.BindingContext.ParseResult.GetValueForArgument(LoginCommand.TokenArgument);
        await token.Write(token1, context.GetCancellationToken());

        var agent = await client.GetAgent(context.GetCancellationToken());
        Console.WriteLine($"Welcome, {agent.Id}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}