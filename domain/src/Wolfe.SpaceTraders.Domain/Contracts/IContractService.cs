using Wolfe.SpaceTraders.Domain.Contracts.Results;

namespace Wolfe.SpaceTraders.Domain.Contracts;

/// <summary>
/// Provides functionality for interacting with contracts.
/// </summary>
public interface IContractService
{
    /// <summary>
    /// Accepts the contract with the specified ID.
    /// </summary>
    /// <param name="contractId">The ID of the contract to accept.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The accepted contract details.</returns>
    public Task<AcceptContractResult> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the contract with the specified ID.
    /// </summary>
    /// <param name="contractId">The ID of the contract tro accept.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>Gets the contract if it exists, otherwise null.</returns>
    public Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all the contracts available for the current agent.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to cancel the operation.</param>
    /// <returns>The discovered contracts.</returns>
    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default);
}