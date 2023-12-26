using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Sdk.Models.Ships;

namespace Wolfe.SpaceTraders.Infrastructure.Api.Extensions;

internal static class SpaceTradersCargoItemExtensions
{
    public static ShipCargoItem ToDomain(this SpaceTradersCargoItem item) => new()
    {
        Symbol = new TradeSymbol(item.Symbol),
        Name = item.Name,
        Description = item.Description,
        Units = item.Units,
    };
}