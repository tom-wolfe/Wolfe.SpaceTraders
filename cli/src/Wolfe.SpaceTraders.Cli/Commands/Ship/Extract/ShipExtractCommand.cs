using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Extract;

internal static class ShipExtractCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new("ship-id", r => new ShipSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "extract",
            description: "Instructs the given ship to extract resources at its current location."
        );
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipExtractCommandHandler>().InvokeAsync(context));

        return command;
    }
}