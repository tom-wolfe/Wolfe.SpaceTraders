using Wolfe.SpaceTraders.Sdk.Models.Extraction;
using Wolfe.SpaceTraders.Service.Results;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipExtractResultExtensions
{
    public static ShipExtractResult ToDomain(this SpaceTradersShipExtractResult result) => new()
    {
        Cooldown = result.Cooldown.ToDomain(),
        Yield = result.Extraction.Yield.ToDomain(),
        Cargo = result.Cargo.ToDomain(),
    };
}