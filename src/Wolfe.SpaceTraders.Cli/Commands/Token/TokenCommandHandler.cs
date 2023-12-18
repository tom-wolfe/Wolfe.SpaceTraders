using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Token;

namespace Wolfe.SpaceTraders.Cli.Commands.Token;

internal class TokenCommandHandler : CommandHandler
{
    private readonly ITokenReader _tokenReader;

    public TokenCommandHandler(ITokenReader tokenReader)
    {
        _tokenReader = tokenReader;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        try
        {
            var token = await _tokenReader.Read(context.GetCancellationToken());
            if (token == null)
            {
                Console.WriteLine("You are not current logged in.".Color(ConsoleColors.Warning));
            }
            else
            {
                Console.WriteLine(token.Color(ConsoleColors.Code));
            }

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting system: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}