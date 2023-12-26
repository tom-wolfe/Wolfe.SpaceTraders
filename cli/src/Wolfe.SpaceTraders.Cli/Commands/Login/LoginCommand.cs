using System.CommandLine;

namespace Wolfe.SpaceTraders.Cli.Commands.Login;

internal static class LoginCommand
{
    public static readonly Argument<string> TokenArgument = new(
        name: "token",
        description: "The access token to use when calling the SpaceTraders API."
    )
    {
        Arity = ArgumentArity.ExactlyOne
    };

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "login",
            description: "Logs in to the CLI and caches the token."
        );
        command.AddArgument(TokenArgument);
        command.SetHandler(context => services.GetRequiredService<LoginCommandHandler>().InvokeAsync(context));

        return command;
    }
}