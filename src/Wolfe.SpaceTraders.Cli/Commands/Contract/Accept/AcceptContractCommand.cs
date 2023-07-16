using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Contract.Accept;

internal static class AcceptContractCommand
{
    public static readonly Argument<ContractId> ContractIdArgument = new("contract-id", r => new ContractId(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("accept");
        command.AddArgument(ContractIdArgument);
        command.SetHandler(context => services.GetRequiredService<AcceptContractCommandHandler>().InvokeAsync(context));

        return command;
    }
}