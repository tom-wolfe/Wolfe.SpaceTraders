using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Contracts;

internal class ContractsCommandHandler(IContractService service) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var contracts = service
            .GetContracts(context.GetCancellationToken());

        await foreach (var contract in contracts)
        {
            ContractFormatter.WriteContract(contract);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}