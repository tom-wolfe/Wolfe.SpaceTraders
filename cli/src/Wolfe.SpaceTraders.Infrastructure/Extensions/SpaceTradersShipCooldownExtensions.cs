using Wolfe.SpaceTraders.Domain.Models.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersShipCooldownExtensions
{
    public static ShipCooldown ToDomain(this SpaceTradersShipCooldown cooldown) => new()
    {
        Expiration = cooldown.Expiration,
        Total = TimeSpan.FromSeconds(cooldown.TotalSeconds),
    };
}