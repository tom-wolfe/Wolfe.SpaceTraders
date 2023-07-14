using Refit;
using Wolfe.SpaceTraders.Models;
using Wolfe.SpaceTraders.Responses;

namespace Wolfe.SpaceTraders;

[Headers("Authorization: Bearer")]
public interface ISpaceTradersClient
{
    [Get("/my/agent")]
    public Task<IApiResponse<SpaceTradersResponse<Agent>>> GetAgent(CancellationToken cancellationToken);
}