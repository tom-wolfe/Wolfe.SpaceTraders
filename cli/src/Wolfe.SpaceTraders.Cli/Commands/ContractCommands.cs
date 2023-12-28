using Cocona;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ContractCommands(IContractService contractService)
{
    public async Task<int> Accept([Argument] ContractId contractId, CancellationToken cancellationToken = default)
    {
        var contract = await contractService.GetContract(contractId, cancellationToken) ?? throw new Exception($"Contract {contractId} could not be found.");

        await contract.Accept(cancellationToken);
        Console.WriteLine("Accepted contract successfully.".Color(ConsoleColors.Success));

        return ExitCodes.Success;
    }

    public async Task<int> Contract([Argument] ContractId contractId, CancellationToken cancellationToken = default)
    {
        var contract = await contractService.GetContract(contractId, cancellationToken) ?? throw new Exception($"Contract '{contractId}' not found.");
        ContractFormatter.WriteContract(contract);

        return ExitCodes.Success;
    }

    public async Task<int> Contracts(CancellationToken cancellationToken = default)
    {
        var contracts = contractService.GetContracts(cancellationToken);
        await foreach (var contract in contracts)
        {
            ContractFormatter.WriteContract(contract);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}
