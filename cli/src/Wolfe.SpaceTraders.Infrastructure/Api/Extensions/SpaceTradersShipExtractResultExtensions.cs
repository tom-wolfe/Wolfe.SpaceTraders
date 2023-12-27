using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Extraction;

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