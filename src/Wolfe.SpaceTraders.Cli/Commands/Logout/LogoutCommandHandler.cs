using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Token;

namespace Wolfe.SpaceTraders.Cli.Commands.Logout;

internal class LogoutCommandHandler(ITokenService token) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        await token.Clear(context.GetCancellationToken());
        Console.WriteLine("Logged out successfully.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}