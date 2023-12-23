using Wolfe.SpaceTraders.Sdk.Models;
using Wolfe.SpaceTraders.Sdk.Models.Contracts;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public class SpaceTradersGetContractsResponse : ISpaceTradersListResponse<SpaceTradersContract>
{
    public required IEnumerable<SpaceTradersContract> Data { get; set; }
    public required SpaceTradersListMeta Meta { get; set; }
}