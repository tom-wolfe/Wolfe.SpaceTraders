using System.CommandLine;
using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Cli.Extensions;

internal static class CommandExtensions
{
    public static void SetProvidedHandler<T>(this Command command, IServiceProvider provider) where T : ICommandHandler
    {
        command.SetHandler(context => provider.GetRequiredService<T>().InvokeAsync(context));
    }
}