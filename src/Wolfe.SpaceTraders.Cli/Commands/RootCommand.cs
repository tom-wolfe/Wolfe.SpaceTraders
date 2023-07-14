using System.CommandLine;
using Wolfe.SpaceTraders.Commands.Login;
using Wolfe.SpaceTraders.Commands.Logout;
using Wolfe.SpaceTraders.Commands.Me;

namespace Wolfe.SpaceTraders.Commands;

internal static class RootCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new System.CommandLine.RootCommand();
        command.AddCommand(LoginCommand.CreateCommand(services));
        command.AddCommand(LogoutCommand.CreateCommand(services));
        command.AddCommand(MeCommand.CreateCommand(services));

        return command;
    }
}