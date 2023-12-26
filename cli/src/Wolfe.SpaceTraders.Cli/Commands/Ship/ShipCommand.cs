using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Commands.Ship.Dock;
using Wolfe.SpaceTraders.Cli.Commands.Ship.Extract;
using Wolfe.SpaceTraders.Cli.Commands.Ship.Navigate;
using Wolfe.SpaceTraders.Cli.Commands.Ship.Orbit;
using Wolfe.SpaceTraders.Cli.Commands.Ship.Refuel;
using Wolfe.SpaceTraders.Cli.Commands.Ship.Sell;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship;

internal static class ShipCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new("ship-id", r => new ShipSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "ship",
            description: "Displays the details of the given ship."
        );
        command.AddArgument(ShipIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipCommandHandler>().InvokeAsync(context));

        command.AddCommand(ShipDockCommand.CreateCommand(services));
        command.AddCommand(ShipExtractCommand.CreateCommand(services));
        command.AddCommand(ShipNavigateCommand.CreateCommand(services));
        command.AddCommand(ShipOrbitCommand.CreateCommand(services));
        command.AddCommand(ShipRefuelCommand.CreateCommand(services));
        command.AddCommand(ShipSellCommand.CreateCommand(services));

        return command;
    }
}