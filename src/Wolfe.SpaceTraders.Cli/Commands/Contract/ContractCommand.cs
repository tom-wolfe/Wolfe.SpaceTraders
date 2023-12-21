using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Commands.Contract.Accept;
using Wolfe.SpaceTraders.Domain.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Contract;

internal static class ContractCommand
{
    public static readonly Argument<ContractId> ContractIdArgument = new(
        name: "contract-id",
        parse: r =>
        {
            var contractId = r.Tokens.Select(t => t.Value).First();
            return new ContractId(contractId);
        }
    )
    {
        Arity = ArgumentArity.ExactlyOne
    };

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