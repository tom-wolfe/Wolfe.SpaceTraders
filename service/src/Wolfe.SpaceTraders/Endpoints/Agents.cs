using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Agents.Commands;
using Wolfe.SpaceTraders.Infrastructure.Agents;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Agents
{
    public static WebApplication MapAgentEndpoints(this WebApplication app)
    {
        var agentsGroup = app.MapGroup("/agents");

        agentsGroup.MapPost("/", (IAgentService agentService, CreateAgentCommand command, CancellationToken cancellationToken = default) => agentService.CreateAgent(command, cancellationToken));
        agentsGroup.MapGet("/me", (IAgentService agentService, CancellationToken cancellationToken = default) => agentService.GetAgent(cancellationToken));
        agentsGroup.MapPost("/login", async (IAgentService agentService, ITokenService tokenService, string token, CancellationToken cancellationToken = default) =>
        {
            await tokenService.SetAccessToken(token, cancellationToken);
            var agent = await agentService.GetAgent(cancellationToken);
            return agent;
        });

        return app;
    }
}