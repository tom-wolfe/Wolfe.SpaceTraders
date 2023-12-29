using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Navigation;
using Wolfe.SpaceTraders.Sdk.Models.Systems;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersSystemExtensions
{
    public static StarSystem ToDomain(this SpaceTradersSystem system) => new()
    {
        Id = new SystemId(system.Symbol),
        Type = new SystemType(system.Type),
        Location = new Point(system.X, system.Y),
    };
}