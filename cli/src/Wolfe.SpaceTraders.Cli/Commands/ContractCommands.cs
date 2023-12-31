using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Contracts;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ContractCommands(IContractService contractService, IHostApplicationLifetime host)
{
    public async Task<int> List()
    {
        var contracts = contractService.GetContracts(host.ApplicationStopping);
        await foreach (var contract in contracts)
        {
            ContractFormatter.WriteContract(contract);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }

    public async Task<int> Get([Argument] ContractId contractId)
    {
        var contract = await contractService.GetContract(contractId, host.ApplicationStopping) ?? throw new Exception($"Contract '{contractId}' not found.");
        ContractFormatter.WriteContract(contract);

        return ExitCodes.Success;
    }

    public async Task<int> Accept([Argument] ContractId contractId)
    {
        var contract = await contractService.GetContract(contractId, host.ApplicationStopping) ?? throw new Exception($"Contract {contractId} could not be found.");

        await contract.Accept(host.ApplicationStopping);
        ConsoleHelpers.WriteLineSuccess($"Accepted contract successfully.");

        return ExitCodes.Success;
    }
}
