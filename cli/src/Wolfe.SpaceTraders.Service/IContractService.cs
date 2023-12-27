namespace Wolfe.SpaceTraders.Domain.Contracts;

public interface IContractService
{
    public Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default);
}