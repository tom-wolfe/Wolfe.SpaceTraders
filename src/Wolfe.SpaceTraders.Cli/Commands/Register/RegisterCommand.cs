using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Register;

internal static class RegisterCommand
{
    public static readonly Argument<AgentSymbol> SymbolArgument = new("symbol", r => new AgentSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static readonly Argument<FactionSymbol> FactionArgument = new("faction", r => new FactionSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static readonly Argument<string?> EmailArgument = new("email", () => null);

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("register");
        command.AddArgument(SymbolArgument);
        command.AddArgument(FactionArgument);
        command.AddArgument(EmailArgument);
        command.SetHandler(context => services.GetRequiredService<RegisterCommandHandler>().InvokeAsync(context));

        return command;
    }
}