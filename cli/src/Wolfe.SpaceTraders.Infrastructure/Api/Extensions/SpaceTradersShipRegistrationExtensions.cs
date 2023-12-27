using Wolfe.SpaceTraders.Domain;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersShipRegistrationExtensions
{
    public static ShipRegistration ToDomain(this SpaceTradersShipRegistration registration) => new()
    {
        FactionId = new FactionId(registration.FactionSymbol),
        Name = registration.Name,
        Role = new ShipRole(registration.Role),
    };
}