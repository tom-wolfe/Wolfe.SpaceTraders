using Refit;
using Wolfe.SpaceTraders.Sdk.Requests;
using Wolfe.SpaceTraders.Sdk.Responses;

namespace Wolfe.SpaceTraders.Sdk;

[Headers("Authorization: Bearer")]
public interface ISpaceTradersApiClient
{
    [Post("/register")]
    [Headers("Authorization:")]
    public Task<IApiResponse<SpaceTradersRegisterResponse>> Register(
        SpaceTradersRegisterRequest request,
        CancellationToken cancellationToken = default
    );

    [Get("/my/agent")]
    public Task<IApiResponse<SpaceTradersGetAgentResponse>> GetAgent(
        CancellationToken cancellationToken = default
    );

    [Get("/my/contracts")]
    public Task<IApiResponse<SpaceTradersGetContractsResponse>> GetContracts(
        [Query] int limit = 10,
        [Query] int page = 1,
        CancellationToken cancellationToken = default
    );

    [Get("/my/contracts/{contractId}")]
    public Task<IApiResponse<SpaceTradersGetContractResponse>> GetContract(
        string contractId,
        CancellationToken cancellationToken = default
    );

    [Post("/my/contracts/{contractId}/accept")]
    public Task<IApiResponse<SpaceTradersAcceptContractResponse>> AcceptContract(
        string contractId,
        CancellationToken cancellationToken = default
    );

    [Get("/systems")]
    public Task<IApiResponse<SpaceTradersGetSystemsResponse>> GetSystems(
        [Query] int limit = 10,
        [Query] int page = 1,
        CancellationToken cancellationToken = default
    );

    [Get("/systems/{systemId}")]
    public Task<IApiResponse<SpaceTradersGetSystemResponse>> GetSystem(
        string systemId,
        CancellationToken cancellationToken = default
    );

    [Get("/systems/{systemId}/waypoints")]
    public Task<IApiResponse<SpaceTradersGetWaypointsResponse>> GetWaypoints(
        string systemId,
        [Query] int limit = 10,
        [Query] int page = 1,
        CancellationToken cancellationToken = default
    );

    [Get("/systems/{systemId}/waypoints/{waypointId}")]
    public Task<IApiResponse<SpaceTradersGetWaypointResponse>> GetWaypoint(
        string systemId,
        string waypointId,
        CancellationToken cancellationToken = default
    );

    [Get("/systems/{systemId}/waypoints/{waypointId}/shipyard")]
    public Task<IApiResponse<SpaceTradersGetShipyardResponse>> GetShipyard(
        string systemId,
        string waypointId,
        CancellationToken cancellationToken = default
    );

    [Get("/systems/{systemId}/waypoints/{waypointId}/market")]
    public Task<IApiResponse<SpaceTradersGetMarketplaceResponse>> GetMarketplace(
        string systemId,
        string waypointId,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships")]
    public Task<IApiResponse<SpaceTradersPurchaseShipResponse>> PurchaseShip(
        SpaceTradersPurchaseShipRequest request,
        CancellationToken cancellationToken = default
    );

    [Get("/my/ships")]
    public Task<IApiResponse<SpaceTradersGetShipsResponse>> GetShips(
        [Query] int limit,
        [Query] int page,
        CancellationToken cancellationToken = default
    );

    [Get("/my/ships/{shipId}")]
    public Task<IApiResponse<SpaceTradersGetShipResponse>> GetShip(
        string shipId,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships/{shipId}/orbit")]
    public Task<IApiResponse<SpaceTradersShipOrbitResponse>> ShipOrbit(
        string shipId,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships/{shipId}/dock")]
    public Task<IApiResponse<SpaceTradersShipDockResponse>> ShipDock(
        string shipId,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships/{shipId}/navigate")]
    public Task<IApiResponse<SpaceTradersShipNavigateResponse>> ShipNavigate(
        string shipId,
        [Body] SpaceTradersShipNavigateRequest request,
        CancellationToken cancellationToken = default
    );

    [Patch("/my/ships/{shipId}/nav")]
    public Task<IApiResponse<SpaceTradersPatchShipNavResponse>> PatchShipNav(
        string shipId,
        [Body] SpaceTradersPatchShipNavRequest request,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships/{shipId}/refuel")]
    Task<IApiResponse<SpaceTradersShipRefuelResponse>> ShipRefuel(
        string shipId,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships/{shipId}/extract")]
    Task<IApiResponse<SpaceTradersShipExtractResponse>> ShipExtract(
        string shipId,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships/{shipId}/jettison")]
    Task<IApiResponse<SpaceTradersShipJettisonResponse>> ShipJettison(
        string shipId,
        SpaceTradersShipJettisonRequest request,
        CancellationToken cancellationToken = default
    );

    [Post("/my/ships/{shipId}/sell")]
    Task<IApiResponse<SpaceTradersShipSellResponse>> ShipSell(
        string shipId,
        SpaceTradersShipSellRequest request,
        CancellationToken cancellationToken = default
    );
}
