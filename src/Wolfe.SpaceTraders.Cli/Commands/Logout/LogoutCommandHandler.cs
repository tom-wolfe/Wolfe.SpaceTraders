using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Token;

namespace Wolfe.SpaceTraders.Cli.Commands.Logout;

internal class LogoutCommandHandler : CommandHandler
{
    private readonly ITokenWriter _token;

    public LogoutCommandHandler(ITokenWriter token)
    {
        _token = token;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        await _token.Clear(context.GetCancellationToken());
        Console.WriteLine("Logged out successfully.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}