using Wolfe.SpaceTraders.Domain.Models;
using Wolfe.SpaceTraders.Sdk.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersWaypointTraitExtensions
{
    public static WaypointTrait ToDomain(this SpaceTradersWaypointTrait trait) => new()
    {
        Symbol = new WaypointTraitSymbol(trait.Symbol),
        Name = trait.Name,
        Description = trait.Description,
    };
}