using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Purchase.First;

internal static class PurchaseFirstShipCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("first");
        command.SetHandler(context => services.GetRequiredService<PurchaseFirstShipCommandHandler>().InvokeAsync(context));

        return command;
    }
}