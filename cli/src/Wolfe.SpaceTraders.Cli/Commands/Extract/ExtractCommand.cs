using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Extract;

internal static class ExtractCommand
{
    public static readonly Argument<ShipId> ShipIdArgument = new("ship-id", r => new ShipId(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "extract",
            description: "Instructs the given ship to extract resources at its current location."
        );
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ExtractCommandHandler>().InvokeAsync(context));

        return command;
    }
}