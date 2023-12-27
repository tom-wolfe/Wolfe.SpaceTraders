using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersCargoItemExtensions
{
    public static ShipCargoItem ToDomain(this SpaceTradersCargoItem item) => new()
    {
        Id = new TradeId(item.Symbol),
        Name = item.Name,
        Description = item.Description,
        Quantity = item.Units,
    };
}