using System.CommandLine;
using Wolfe.SpaceTraders.Commands.Contracts.Accept;

namespace Wolfe.SpaceTraders.Commands.Contract;

internal static class ContractCommand
{
    public static readonly Argument<string> IdArgument = new("id");

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("contract");
        command.AddArgument(IdArgument);
        command.SetHandler(context => services.GetRequiredService<ContractCommandHandler>().InvokeAsync(context));

        command.AddCommand(AcceptContractCommand.CreateCommand(services));

        return command;
    }
}