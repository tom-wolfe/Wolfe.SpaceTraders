using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Shipyard;

internal static class ShipyardCommand
{
    public static readonly Argument<string> ShipyardIdArgument = new("shipyard-id");

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("shipyard");
        command.AddArgument(ShipyardIdArgument);
        command.SetHandler(context => services.GetRequiredService<ShipyardCommandHandler>().InvokeAsync(context));

        return command;
    }
}