﻿using Wolfe.SpaceTraders.Domain.Contracts;

namespace Wolfe.SpaceTraders.Service.Contracts;

public interface IContractService
{
    public Task<Contract?> GetContract(ContractId contractId, CancellationToken cancellationToken = default);
    public IAsyncEnumerable<Contract> GetContracts(CancellationToken cancellationToken = default);
}