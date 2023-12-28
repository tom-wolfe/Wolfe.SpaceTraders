using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Service.Commands;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Service;

public interface IAgentService
{
    public Task<Agent> GetAgent(CancellationToken cancellationToken = default);
    public Task<RegisterResult> CreateAgent(CreateAgentCommand command, CancellationToken cancellationToken = default);
}