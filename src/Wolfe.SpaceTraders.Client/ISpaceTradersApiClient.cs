using Refit;
using Wolfe.SpaceTraders.Models;
using Wolfe.SpaceTraders.Requests;
using Wolfe.SpaceTraders.Responses;

namespace Wolfe.SpaceTraders;

[Headers("Authorization: Bearer")]
internal interface ISpaceTradersApiClient
{
    [Get("/my/agent")]
    public Task<IApiResponse<SpaceTradersResponse<Agent>>> GetAgent(CancellationToken cancellationToken = default);

    [Get("/my/contracts?limit={limit}&page={page}")]
    public Task<IApiResponse<SpaceTradersListResponse<Contract>>> GetContracts(int limit = 10, int page = 1, CancellationToken cancellationToken = default);

    [Get("/my/contracts/{contractId}")]
    public Task<IApiResponse<SpaceTradersResponse<Contract>>> GetContract(ContractId contractId, CancellationToken cancellationToken = default);

    [Post("/my/contracts/{contractId}/accept")]
    public Task<IApiResponse<SpaceTradersResponse<AcceptContractResponse>>> AcceptContract(ContractId contractId, CancellationToken cancellationToken = default);

    [Get("/systems?limit={limit}&page={page}")]
    public Task<IApiResponse<SpaceTradersListResponse<ShallowStarSystem>>> GetSystems(int limit = 10, int page = 1, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}")]
    public Task<IApiResponse<SpaceTradersResponse<StarSystem>>> GetSystem(SystemSymbol systemId, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}/waypoints?limit={limit}&page={page}")]
    public Task<IApiResponse<SpaceTradersListResponse<ShallowWaypoint>>> GetWaypoints(SystemSymbol systemId, int limit = 10, int page = 1, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}/waypoints/{waypointId}")]
    public Task<IApiResponse<SpaceTradersResponse<Waypoint>>> GetWaypoint(SystemSymbol systemId, WaypointSymbol waypointId, CancellationToken cancellationToken = default);

    [Get("/systems/{systemId}/waypoints/{waypointId}/shipyard")]
    public Task<IApiResponse<SpaceTradersResponse<Shipyard>>> GetShipyard(SystemSymbol systemId, WaypointSymbol waypointId, CancellationToken cancellationToken = default);

    [Post("/my/ships")]
    public Task<IApiResponse<SpaceTradersResponse<PurchaseShipResponse>>> PurchaseShip(PurchaseShipRequest request, CancellationToken cancellationToken = default);

    [Get("/my/ships")]
    public Task<IApiResponse<SpaceTradersListResponse<ShallowShip>>> GetShips(int limit, int page, CancellationToken cancellationToken = default);

    [Get("/my/ships/{shipId}")]
    public Task<IApiResponse<SpaceTradersResponse<Ship>>> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default);
}
