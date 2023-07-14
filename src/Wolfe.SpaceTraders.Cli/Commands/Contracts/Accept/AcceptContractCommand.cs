using System.CommandLine;

namespace Wolfe.SpaceTraders.Commands.Contracts.Accept;

internal static class AcceptContractCommand
{
    public static readonly Argument<string> IdArgument = new("id");
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("accept");
        command.AddArgument(IdArgument);
        command.SetHandler(context => services.GetRequiredService<AcceptContractCommandHandler>().InvokeAsync(context));

        return command;
    }
}