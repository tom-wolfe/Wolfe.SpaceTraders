using Wolfe.SpaceTraders.Domain.Agents;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Agents
{
    public static WebApplication MapAgentEndpoints(this WebApplication app)
    {
        app.MapGet("/agents/me", async (IAgentService agentService, CancellationToken cancellationToken = default) =>
        {
            var agent = await agentService.GetAgent(cancellationToken);
            return agent;
        });
        return app;
    }
}