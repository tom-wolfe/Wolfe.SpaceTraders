using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Commands.Clear;
using Wolfe.SpaceTraders.Cli.Commands.Contract;
using Wolfe.SpaceTraders.Cli.Commands.Contracts;
using Wolfe.SpaceTraders.Cli.Commands.Login;
using Wolfe.SpaceTraders.Cli.Commands.Logout;
using Wolfe.SpaceTraders.Cli.Commands.Me;
using Wolfe.SpaceTraders.Cli.Commands.Purchase;
using Wolfe.SpaceTraders.Cli.Commands.Register;
using Wolfe.SpaceTraders.Cli.Commands.Ship;
using Wolfe.SpaceTraders.Cli.Commands.Ships;
using Wolfe.SpaceTraders.Cli.Commands.Shipyard;
using Wolfe.SpaceTraders.Cli.Commands.System;
using Wolfe.SpaceTraders.Cli.Commands.Systems;
using Wolfe.SpaceTraders.Cli.Commands.Waypoint;
using Wolfe.SpaceTraders.Cli.Commands.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal static class RootCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new global::System.CommandLine.RootCommand();
        command.AddCommand(ClearCommand.CreateCommand(services));
        command.AddCommand(LoginCommand.CreateCommand(services));
        command.AddCommand(LogoutCommand.CreateCommand(services));
        command.AddCommand(MeCommand.CreateCommand(services));
        command.AddCommand(ContractCommand.CreateCommand(services));
        command.AddCommand(ContractsCommand.CreateCommand(services));
        command.AddCommand(PurchaseCommand.CreateCommand(services));
        command.AddCommand(RegisterCommand.CreateCommand(services));
        command.AddCommand(ShipCommand.CreateCommand(services));
        command.AddCommand(ShipsCommand.CreateCommand(services));
        command.AddCommand(ShipyardCommand.CreateCommand(services));
        command.AddCommand(SystemCommand.CreateCommand(services));
        command.AddCommand(SystemsCommand.CreateCommand(services));
        command.AddCommand(WaypointCommand.CreateCommand(services));
        command.AddCommand(WaypointsCommand.CreateCommand(services));

        return command;
    }
}