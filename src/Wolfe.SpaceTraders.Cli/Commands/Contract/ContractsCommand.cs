using System.CommandLine;
using Wolfe.SpaceTraders.Commands.Contract.Accept;
using Wolfe.SpaceTraders.Models;

namespace Wolfe.SpaceTraders.Commands.Contract;

internal static class ContractCommand
{
    public static readonly Argument<ContractId> ContractIdArgument = new("contract-id", r => new ContractId(string.Join(' ', r.Tokens.Select(t => t.Value))));

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("contract");
        command.AddArgument(ContractIdArgument);
        command.SetHandler(context => services.GetRequiredService<ContractCommandHandler>().InvokeAsync(context));

        command.AddCommand(AcceptContractCommand.CreateCommand(services));

        return command;
    }
}