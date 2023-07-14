using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Token;

namespace Wolfe.SpaceTraders.Commands.Login;

internal class LoginCommandHandler : CommandHandler
{
    private readonly ITokenSetService _token;

    public LoginCommandHandler(ITokenSetService token)
    {
        _token = token;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var token = context.BindingContext.ParseResult.GetValueForArgument(LoginCommand.TokenArgument);
        await _token.SetToken(token, context.GetCancellationToken());
        return ExitCodes.Success;
    }
}