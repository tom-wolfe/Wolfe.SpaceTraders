using Wolfe.SpaceTraders.Domain.Ships.Results;
using Wolfe.SpaceTraders.Sdk.Models.Jettison;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipJettisonResultExtensions
{
    public static ShipJettisonResult ToDomain(this SpaceTradersShipJettisonResult result) => new()
    {
        Cargo = result.Cargo.ToDomain()
    };
}