using Wolfe.SpaceTraders.Domain.Agents.Commands;
using Wolfe.SpaceTraders.Domain.Agents.Results;

namespace Wolfe.SpaceTraders.Domain.Agents;

public interface IAgentService
{
    public Task<Agent> GetAgent(CancellationToken cancellationToken = default);
    public Task<CreateAgentResult> CreateAgent(CreateAgentCommand command, CancellationToken cancellationToken = default);
}