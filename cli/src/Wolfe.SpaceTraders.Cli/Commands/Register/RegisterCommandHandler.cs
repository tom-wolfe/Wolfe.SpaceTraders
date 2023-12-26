using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Register;

internal class RegisterCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var symbol = context.BindingContext.ParseResult.GetValueForArgument(RegisterCommand.SymbolArgument);
        var faction = context.BindingContext.ParseResult.GetValueForOption(RegisterCommand.FactionOption);
        var email = context.BindingContext.ParseResult.GetValueForOption(RegisterCommand.EmailOption);

        var request = new Service.Commands.RegisterCommand
        {
            Symbol = symbol,
            Faction = faction ?? FactionSymbol.Cosmic, // Default faction.
            Email = email
        };
        var response = await client.Register(request, context.GetCancellationToken());

        Console.WriteLine(response.Token);
        Console.WriteLine($"Welcome, {response.Agent.Symbol}!".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }
}