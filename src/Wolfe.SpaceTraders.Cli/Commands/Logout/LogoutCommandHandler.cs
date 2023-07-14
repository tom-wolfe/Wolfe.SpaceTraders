using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Token;

namespace Wolfe.SpaceTraders.Commands.Logout;

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
        return ExitCodes.Success;
    }
}