using System.CommandLine;
using Wolfe.SpaceTraders.Commands.Clear;
using Wolfe.SpaceTraders.Commands.Contract;
using Wolfe.SpaceTraders.Commands.Contracts;
using Wolfe.SpaceTraders.Commands.Login;
using Wolfe.SpaceTraders.Commands.Logout;
using Wolfe.SpaceTraders.Commands.Me;
using Wolfe.SpaceTraders.Commands.Shipyard;
using Wolfe.SpaceTraders.Commands.System;
using Wolfe.SpaceTraders.Commands.Systems;
using Wolfe.SpaceTraders.Commands.Waypoint;
using Wolfe.SpaceTraders.Commands.Waypoints;

namespace Wolfe.SpaceTraders.Commands;

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
        command.AddCommand(ShipyardCommand.CreateCommand(services));
        command.AddCommand(SystemCommand.CreateCommand(services));
        command.AddCommand(SystemsCommand.CreateCommand(services));
        command.AddCommand(WaypointCommand.CreateCommand(services));
        command.AddCommand(WaypointsCommand.CreateCommand(services));

        return command;
    }
}