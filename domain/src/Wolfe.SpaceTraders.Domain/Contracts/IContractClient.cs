using Wolfe.SpaceTraders.Domain.Contracts.Results;

namespace Wolfe.SpaceTraders.Domain.Contracts;

public interface IContractClient
{
    public Task<AcceptContractResult> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default);
}