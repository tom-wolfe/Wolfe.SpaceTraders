using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Commands.Contract.Accept;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Contract;

internal static class ContractCommand
{
    public static readonly Argument<ContractId> ContractIdArgument = new("contract-id", r => new ContractId(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "contract",
            description: "Displays the details of a contract."
        );
        command.AddArgument(ContractIdArgument);
        command.SetHandler(context => services.GetRequiredService<ContractCommandHandler>().InvokeAsync(context));

        command.AddCommand(AcceptContractCommand.CreateCommand(services));

        return command;
    }
}