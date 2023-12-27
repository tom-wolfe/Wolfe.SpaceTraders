using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure;

internal class SpaceTradersContractClient(ISpaceTradersApiClient apiClient) : IContractClient
{
    public async Task<AcceptContractResult> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.AcceptContract(contractId.Value, cancellationToken);
        return response.GetContent().Data.ToDomain(this);
    }
}