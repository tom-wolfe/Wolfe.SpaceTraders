using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Domain.Models.Marketplace;
using Wolfe.SpaceTraders.Domain.Models.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Sell;

internal static class ShipSellCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new(
        name: "ship",
        parse: r => new ShipSymbol(r.Tokens[0].Value),
        description: "The ID of the ship to sell cargo from."
    );

    public static readonly Argument<TradeSymbol> ItemIdArgument = new(
        name: "item",
        parse: r => new TradeSymbol(r.Tokens[0].Value),
        description: "The item to sell."
    );

    public static readonly Argument<int> QuantityArgument = new(
        name: "quantity",
        description: "The amount of the item to sell."
    );

    public static Command CreateCommand(IServiceProvider provider)
    {
        var command = new Command(
            name: "sell",
            description: "Sells cargo from a ship's hold."
        );
        command.AddArgument(ShipIdArgument);
        command.AddArgument(ItemIdArgument);
        command.AddArgument(QuantityArgument);
        command.SetProvidedHandler<ShipSellCommandHandler>(provider);

        return command;
    }
}