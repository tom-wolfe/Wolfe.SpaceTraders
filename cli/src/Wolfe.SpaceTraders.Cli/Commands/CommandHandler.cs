using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal abstract class CommandHandler : ICommandHandler
{
    public int Invoke(InvocationContext context) => InvokeAsync(context).GetAwaiter().GetResult();

    public abstract Task<int> InvokeAsync(InvocationContext context);
}