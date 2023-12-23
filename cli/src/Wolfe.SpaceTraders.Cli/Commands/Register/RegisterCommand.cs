using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Agents;

namespace Wolfe.SpaceTraders.Cli.Commands.Register;

internal static class RegisterCommand
{
    public static readonly Argument<AgentSymbol> SymbolArgument = new(
        name: "symbol",
        parse: r => new AgentSymbol(r.Tokens.Select(t => t.Value).First()),
        description: "Your desired agent symbol. This will be a unique name used to represent your agent, and will be the prefix for your ships."
    );

    public static readonly Option<FactionSymbol?> FactionOption = new(
        aliases: ["-f", "--faction"],
        parseArgument: r =>
        {
            var faction = r.Tokens.Select(t => t.Value).FirstOrDefault()?.ToString();
            if (string.IsNullOrWhiteSpace(faction)) { return null; }
            return new FactionSymbol(faction);
        },
        description: "The symbol of the faction the agent will belong to."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string?> EmailOption = new(
        aliases: ["-e", "--email"],
        getDefaultValue: () => null,
        description: "Your email address. This is used if you reserved your call sign between resets."
    )
    {
        IsRequired = false
    };

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("register", "Creates a new agent and ties it to an account.");
        command.AddArgument(SymbolArgument);
        command.AddOption(FactionOption);
        command.AddOption(EmailOption);
        command.SetHandler(context => services.GetRequiredService<RegisterCommandHandler>().InvokeAsync(context));

        return command;
    }
}