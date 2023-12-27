using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Sell;

internal static class SellCommand
{
    public static readonly Argument<ShipId> ShipIdArgument = new(
        name: "ship",
        parse: r => new ShipId(r.Tokens[0].Value),
        description: "The ID of the ship to sell cargo from."
    );

    public static readonly Argument<ItemId> ItemIdArgument = new(
        name: "item",
        parse: r => new ItemId(r.Tokens[0].Value),
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
        command.SetProvidedHandler<SellCommandHandler>(provider);

        return command;
    }
}