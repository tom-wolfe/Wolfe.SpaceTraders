using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Contracts.Results;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Sdk;

namespace Wolfe.SpaceTraders.Infrastructure.Contracts;

internal class SpaceTradersContractClient(ISpaceTradersApiClient apiClient) : IContractClient
{
    public async Task<AcceptContractResult> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.AcceptContract(contractId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain(this);
    }
}