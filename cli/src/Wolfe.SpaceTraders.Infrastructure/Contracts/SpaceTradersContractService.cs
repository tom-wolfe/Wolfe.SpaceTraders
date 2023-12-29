using System.Net;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;
using Wolfe.SpaceTraders.Service.Contracts;

namespace Wolfe.SpaceTraders.Infrastructure.Contracts;

internal class SpaceTradersContractService(
    ISpaceTradersApiClient apiClient,
    IContractClient contractClient
) : IContractService
{
    public async Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetContract(contractId.Value, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound) { return null; }
        return response.GetContent().Data.ToDomain(contractClient);
    }

    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default)
    {
        return PaginationHelpers.ToAsyncEnumerable<SpaceTradersContract>(
            async p => (await apiClient.GetContracts(20, p, cancellationToken)).GetContent()
        ).SelectAwait(c => ValueTask.FromResult(c.ToDomain(contractClient)));
    }
}