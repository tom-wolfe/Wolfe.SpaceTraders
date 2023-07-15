using System.CommandLine;
using Wolfe.SpaceTraders.Commands.Contracts.Accept;

namespace Wolfe.SpaceTraders.Commands.Contracts;

internal static class ContractsCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("contracts");
        command.SetHandler(context => services.GetRequiredService<ContractsCommandHandler>().InvokeAsync(context));

        command.AddCommand(AcceptContractCommand.CreateCommand(services));

        return command;
    }
}