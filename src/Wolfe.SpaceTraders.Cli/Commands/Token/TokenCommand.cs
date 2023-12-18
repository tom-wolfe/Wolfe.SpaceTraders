using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Token;

internal static class TokenCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "token",
            description: "Outputs the token being used to connect to the SpaceTraders API."
        );
        command.SetHandler(context => services.GetRequiredService<TokenCommandHandler>().InvokeAsync(context));

        return command;
    }
}