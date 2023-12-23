using Wolfe.SpaceTraders.Domain.Models.Marketplace;
using Wolfe.SpaceTraders.Sdk.Models.Marketplace;

namespace Wolfe.SpaceTraders.Infrastructure.Extensions;

internal static class SpaceTradersMarketItemExtensions
{
    public static MarketItem ToDomain(this SpaceTradersMarketItem item) => new()
    {
        Symbol = new TradeSymbol(item.Symbol),
        Name = item.Name,
        Description = item.Description
    };
}