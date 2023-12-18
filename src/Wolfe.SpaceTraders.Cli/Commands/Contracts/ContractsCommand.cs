using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Commands.Contract.Accept;

namespace Wolfe.SpaceTraders.Cli.Commands.Contracts;

internal static class ContractsCommand
{
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "contracts",
            description: "Displays the list of available contracts."
        );
        command.SetHandler(context => services.GetRequiredService<ContractsCommandHandler>().InvokeAsync(context));

        command.AddCommand(AcceptContractCommand.CreateCommand(services));

        return command;
    }
}