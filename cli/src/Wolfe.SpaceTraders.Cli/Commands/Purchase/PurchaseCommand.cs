using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Commands.Purchase;

internal static class PurchaseCommand
{
    public static readonly Argument<WaypointId> ShipyardIdArgument = new("shipyard-id", r => new WaypointId(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static readonly Argument<ShipType> ShipTypeArgument = new("ship-type", r => new ShipType(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "purchase",
            description: "Purchases a ship."
        );
        command.AddArgument(ShipyardIdArgument);
        command.AddArgument(ShipTypeArgument);
        command.SetHandler(context => services.GetRequiredService<PurchaseCommandHandler>().InvokeAsync(context));

        return command;
    }
}
