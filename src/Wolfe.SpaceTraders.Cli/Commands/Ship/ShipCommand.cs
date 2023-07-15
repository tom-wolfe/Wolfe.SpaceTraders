using System.CommandLine;
using Wolfe.SpaceTraders.Models;

namespace Wolfe.SpaceTraders.Commands.Ship;

internal static class ShipCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new("ship-id", r => new ShipSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("ship");
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipCommandHandler>().InvokeAsync(context));

        return command;
    }
}