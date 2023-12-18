using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Purchase.First;

internal static class PurchaseFirstShipCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "first",
            description: "Purchases a mining drone from the nearest shipyard."
        );
        command.SetHandler(context => services.GetRequiredService<PurchaseFirstShipCommandHandler>().InvokeAsync(context));

        return command;
    }
}