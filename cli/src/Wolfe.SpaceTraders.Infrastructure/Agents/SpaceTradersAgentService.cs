using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Api.Extensions;
using Wolfe.SpaceTraders.Sdk;
using Wolfe.SpaceTraders.Service.Agents;
using Wolfe.SpaceTraders.Service.Agents.Commands;
using Wolfe.SpaceTraders.Service.Agents.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Agents;

internal class SpaceTradersAgentService(
    ISpaceTradersApiClient apiClient,
    IShipClient shipClient,
    IContractClient contractClient
) : IAgentService
{
    public async Task<Agent> GetAgent(CancellationToken cancellationToken = default)
    {
        var response = await apiClient.GetAgent(cancellationToken);
        return response.GetContent().Data.ToDomain();
    }

    public async Task<RegisterResult> CreateAgent(CreateAgentCommand command, CancellationToken cancellationToken = default)
    {

        var response = await apiClient.Register(command.ToApi(), cancellationToken);
        return response.GetContent().Data.ToDomain(shipClient, contractClient);
    }
}