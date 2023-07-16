using Refit;
using Wolfe.SpaceTraders.Core.Models;
using Wolfe.SpaceTraders.Core.Requests;
using Wolfe.SpaceTraders.Core.Responses;

namespace Wolfe.SpaceTraders.Infrastructure;

[Headers("Authorization: Bearer")]
internal interface ISpaceTradersApiClient
{
    [Post("/register")]
    [Headers("Authorization:")]
    public Task<IApiResponse<SpaceTradersResponse<RegisterResponse>>> Register(RegisterRequest request, CancellationToken cancellationToken = default);

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
    public Task<IApiResponse<SpaceTradersListResponse<Ship>>> GetShips(int limit, int page, CancellationToken cancellationToken = default);

    [Get("/my/ships/{shipId}")]
    public Task<IApiResponse<SpaceTradersResponse<Ship>>> GetShip(ShipSymbol shipId, CancellationToken cancellationToken = default);

    [Post("/my/ships/{shipId}/orbit")]
    public Task<IApiResponse<SpaceTradersResponse<ShipOrbitResponse>>> ShipOrbit(ShipSymbol shipId, CancellationToken cancellationToken = default);

    [Post("/my/ships/{shipId}/dock")]
    public Task<IApiResponse<SpaceTradersResponse<ShipDockResponse>>> ShipDock(ShipSymbol shipId, CancellationToken cancellationToken = default);

    [Post("/my/ships/{shipId}/navigate")]
    public Task<IApiResponse<SpaceTradersResponse<ShipNavigateResponse>>> ShipNavigate(ShipSymbol shipId, [Body] ShipNavigateRequest request, CancellationToken cancellationToken = default);

    [Post("/my/ships/{shipId}/refuel")]
    Task<IApiResponse<SpaceTradersResponse<ShipRefuelResponse>>> ShipRefuel(ShipSymbol shipId, CancellationToken cancellationToken = default);

    //[Post("/my/ships/{shipId}/extract")]
    //public Task<IApiResponse<SpaceTradersResponse<ShipExtractResponse>>> ShipExtract(ShipSymbol shipId, [Body] ShipNavigateRequest request, CancellationToken cancellationToken = default);
}
