using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Token;

namespace Wolfe.SpaceTraders.Commands.Register;

internal class RegisterCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;
    private readonly ITokenWriter _token;

    public RegisterCommandHandler(ISpaceTradersClient client, ITokenWriter token)
    {
        _client = client;
        _token = token;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var symbol = context.BindingContext.ParseResult.GetValueForArgument(RegisterCommand.SymbolArgument);
        var faction = context.BindingContext.ParseResult.GetValueForArgument(RegisterCommand.FactionArgument);
        var email = context.BindingContext.ParseResult.GetValueForArgument(RegisterCommand.EmailArgument);

        var request = new RegisterRequest
        {
            Symbol = symbol,
            Faction = faction,
            Email = email
        };
        var response = await _client.Register(request, context.GetCancellationToken());
        await _token.Write(response.Token, context.GetCancellationToken());

        Console.WriteLine($"Welcome, {response.Agent.Symbol}!".Color(ConsoleColors.Information));

        return ExitCodes.Success;
    }
}