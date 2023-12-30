using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Service.Agents.Commands;
using Wolfe.SpaceTraders.Service.Agents.Results;

namespace Wolfe.SpaceTraders.Service.Agents;

public interface IAgentService
{
    public Task<Agent> GetAgent(CancellationToken cancellationToken = default);
    public Task<RegisterResult> CreateAgent(CreateAgentCommand command, CancellationToken cancellationToken = default);
}