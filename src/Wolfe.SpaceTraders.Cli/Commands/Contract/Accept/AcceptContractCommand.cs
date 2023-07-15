using System.CommandLine;
using Wolfe.SpaceTraders.Models;

namespace Wolfe.SpaceTraders.Commands.Contract.Accept;

internal static class AcceptContractCommand
{
    public static readonly Argument<ContractId> IdArgument = new("contract-id", r => new ContractId(string.Join(' ', r.Tokens.Select(t => t.Value))));
    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command("accept");
        command.AddArgument(IdArgument);
        command.SetHandler(context => services.GetRequiredService<AcceptContractCommandHandler>().InvokeAsync(context));

        return command;
    }
}