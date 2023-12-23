using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Cli.Commands.Clear;

internal class ClearCommandHandler : CommandHandler
{
    public override Task<int> InvokeAsync(InvocationContext context)
    {
        Console.Clear();
        return Task.FromResult(ExitCodes.Success);
    }
}