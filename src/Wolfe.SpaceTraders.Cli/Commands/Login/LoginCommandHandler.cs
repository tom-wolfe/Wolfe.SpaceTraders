using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Token;

namespace Wolfe.SpaceTraders.Commands.Login;

internal class LoginCommandHandler : CommandHandler
{
    private readonly ITokenWriter _token;

    public LoginCommandHandler(ITokenWriter token)
    {
        _token = token;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var token = context.BindingContext.ParseResult.GetValueForArgument(LoginCommand.TokenArgument);
        await _token.Write(token, context.GetCancellationToken());
        return ExitCodes.Success;
    }
}