using System.CommandLine;
using Wolfe.SpaceTraders.Models;

namespace Wolfe.SpaceTraders.Commands.Shipyard;

internal static class ShipyardCommand
{
    public static readonly Argument<WaypointSymbol> ShipyardIdArgument = new("shipyard-id", r => new WaypointSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));



    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("shipyard");
        command.AddArgument(ShipyardIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipyardCommandHandler>().InvokeAsync(context));

        return command;
    }
}