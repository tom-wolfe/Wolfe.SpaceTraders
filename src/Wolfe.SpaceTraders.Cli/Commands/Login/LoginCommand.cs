using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Login;

internal static class LoginCommand
{
    public static readonly Argument<string> TokenArgument = new("token");

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("login");
        command.AddArgument(TokenArgument);
        command.SetHandler(context => services.GetRequiredService<LoginCommandHandler>().InvokeAsync(context));

        return command;
    }
}