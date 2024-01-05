using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Agents.Commands;
using Wolfe.SpaceTraders.Domain.Agents.Results;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Sdk;

namespace Wolfe.SpaceTraders.Infrastructure.Agents;

internal class SpaceTradersAgentService(
    ISpaceTradersApiClient apiClient,
    IShipClient shipClient,
    IContractService contractService
) : IAgentService
{
    public async Task<Agent> GetAgent(CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetAgent(cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<CreateAgentResult> CreateAgent(CreateAgentCommand command, CancellationToken cancellationToken = default)
    {
        var response = await apiClient.Register(command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain(shipClient, contractService);
    }
}