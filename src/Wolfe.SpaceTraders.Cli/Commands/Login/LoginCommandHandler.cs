using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Token;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Login;

internal class LoginCommandHandler : CommandHandler
{
    private readonly ITokenWriter _token;
    private readonly ISpaceTradersClient _client;

    public LoginCommandHandler(ITokenWriter token, ISpaceTradersClient client)
    {
        _token = token;
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var token = context.BindingContext.ParseResult.GetValueForArgument(LoginCommand.TokenArgument);
        await _token.Write(token, context.GetCancellationToken());

        var agent = await _client.GetAgent(context.GetCancellationToken());
        Console.WriteLine($"Welcome, {agent.Symbol}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}