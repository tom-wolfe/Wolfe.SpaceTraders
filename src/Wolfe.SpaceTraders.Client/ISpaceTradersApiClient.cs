using Refit;
using Wolfe.SpaceTraders.Models;
using Wolfe.SpaceTraders.Responses;

namespace Wolfe.SpaceTraders;

[Headers("Authorization: Bearer")]
public interface ISpaceTradersApiClient
{
    [Get("/my/agent")]
    public Task<IApiResponse<SpaceTradersResponse<Agent>>> GetAgent(CancellationToken cancellationToken = default);

    [Get("/my/contracts?limit={limit}&page={page}")]
    public Task<IApiResponse<SpaceTradersListResponse<Contract>>> GetContracts(int limit = 10, int page = 1, CancellationToken cancellationToken = default);

    [Get("/my/contracts/{contractId}")]
    public Task<IApiResponse<SpaceTradersResponse<Contract>>> GetContract(string contractId, CancellationToken cancellationToken = default);

    [Post("/my/contracts/{contractId}/accept")]
    public Task<IApiResponse<SpaceTradersResponse<AcceptContractResponse>>> AcceptContract(string contractId, CancellationToken cancellationToken = default);

    [Get("/systems?limit={limit}&page={page}")]
    public Task<IApiResponse<SpaceTradersListResponse<ShallowStarSystem>>> GetSystems(int limit = 10, int page = 1, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}")]
    public Task<IApiResponse<SpaceTradersResponse<StarSystem>>> GetSystem(string systemId, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}/waypoints?limit={limit}&page={page}")]
    public Task<IApiResponse<SpaceTradersListResponse<ShallowWaypoint>>> GetWaypoints(string systemId, int limit = 10, int page = 1, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}/waypoints/{waypointId}")]
    public Task<IApiResponse<SpaceTradersResponse<Waypoint>>> GetWaypoint(string systemId, string waypointId, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}/waypoints/{waypointId}/shipyard")]
    Task<IApiResponse<SpaceTradersResponse<Shipyard>>> GetShipyard(string systemId, string waypointId, CancellationToken cancellationToken = default);
}