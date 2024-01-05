using Wolfe.SpaceTraders.Domain.Contracts;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Contracts
{
    public static WebApplication MapContractEndpoints(this WebApplication app)
    {
        var contractsGroup = app.MapGroup("/contracts");
        contractsGroup.MapGet("/", (IContractService contractService, CancellationToken cancellationToken = default) => contractService.GetContracts(cancellationToken));

        var contractGroup = contractsGroup.MapGroup("/{contractId}");
        contractGroup.MapPost("/", async (IContractService contractService, ContractId contractId, CancellationToken cancellationToken = default) =>
        {
            var system = await contractService.GetContract(contractId, cancellationToken);
            return system == null ? Results.NotFound() : Results.Ok(system);
        });
        contractsGroup.MapPost("/accept", (IContractService contractService, ContractId contractId, CancellationToken cancellationToken = default) => contractService.AcceptContract(contractId, cancellationToken));

        return app;
    }
}