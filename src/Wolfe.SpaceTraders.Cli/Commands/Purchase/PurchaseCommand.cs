using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Commands.Purchase.First;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Purchase;

internal static class PurchaseCommand
{
    public static readonly Argument<WaypointSymbol> ShipyardIdArgument = new("shipyard-id", r => new WaypointSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));
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

        command.AddCommand(PurchaseFirstShipCommand.CreateCommand(services));

        return command;
    }
}
