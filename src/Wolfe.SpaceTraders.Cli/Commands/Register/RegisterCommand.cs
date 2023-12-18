using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Register;

internal static class RegisterCommand
{
    public static readonly Argument<AgentSymbol> SymbolArgument = new(
        name: "symbol",
        parse: r => new AgentSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))),
        description: "Your desired agent symbol. This will be a unique name used to represent your agent, and will be the prefix for your ships."
    );

    public static readonly Argument<FactionSymbol> FactionArgument = new(
        name: "faction",
        parse: r => new FactionSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))),
        description: "The symbol of the faction."
    );

    public static readonly Argument<string?> EmailArgument = new(
        name: "email",
        getDefaultValue: () => null,
        description: "Your email address. This is used if you reserved your call sign between resets."
    );

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("register", "Creates a new agent and ties it to an account.");
        command.AddArgument(SymbolArgument);
        command.AddArgument(FactionArgument);
        command.AddArgument(EmailArgument);
        command.SetHandler(context => services.GetRequiredService<RegisterCommandHandler>().InvokeAsync(context));

        return command;
    }
}