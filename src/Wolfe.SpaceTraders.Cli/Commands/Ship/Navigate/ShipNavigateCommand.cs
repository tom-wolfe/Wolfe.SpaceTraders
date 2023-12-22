using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Navigate;

internal static class ShipNavigateCommand
{
    public static readonly Argument<ShipSymbol> ShipIdArgument = new(
        name: "ship-id",
        parse: r => new ShipSymbol(r.Tokens[0].Value),
        description: "The ID of the ship to navigate."
    );
    public static readonly Argument<WaypointSymbol> WaypointIdArgument = new(
        name: "waypoint-id",
        parse: r => new WaypointSymbol(r.Tokens[0].Value),
        description: "The ID of the waypoint to navigate to."
    );

    public static readonly Option<FlightSpeed?> SpeedOption = new(
        aliases: ["-s", "--speed"],
        parseArgument: r => new FlightSpeed(r.Tokens[0].Value),
        description: "The speed to travel at. (DRIFT, STEALTH, CRUISE or BURN)"
    )
    {
        IsRequired = false,
        Arity = ArgumentArity.ZeroOrOne
    };

    public static Command CreateCommand(IServiceProvider provider)
    {
        var command = new Command(
            name: "navigate",
            description: "Begins navigating the given ship to the given waypoint."
        );
        command.AddArgument(ShipIdArgument);
        command.AddArgument(WaypointIdArgument);
        command.AddOption(SpeedOption);
        command.SetProvidedHandler<ShipNavigateCommandHandler>(provider);

        return command;
    }
}
