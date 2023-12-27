using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersWaypointTraitExtensions
{
    public static WaypointTrait ToDomain(this SpaceTradersWaypointTrait trait) => new()
    {
        Id = new WaypointTraitId(trait.Symbol),
        Name = trait.Name,
        Description = trait.Description,
    };
}