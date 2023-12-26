using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Markets;

internal class MarketsCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var systemId = context.BindingContext.ParseResult.GetValueForArgument(MarketsCommand.SystemIdArgument);
        var selling = context.BindingContext.ParseResult.GetValueForOption(MarketsCommand.SellingOption);
        var buying = context.BindingContext.ParseResult.GetValueForOption(MarketsCommand.BuyingOption);
        var marketplaces = client.GetMarketplaces(systemId, context.GetCancellationToken());

        if (selling != null)
        {
            marketplaces = marketplaces.Where(m => m.IsSelling(selling.Value));
        }

        if (buying != null)
        {
            marketplaces = marketplaces.Where(m => m.IsBuying(buying.Value));
        }

        var location = context.BindingContext.ParseResult.GetValueForOption(MarketsCommand.NearestToOption);
        Domain.Waypoints.Waypoint? relativeWaypoint = null;
        if (location != null)
        {
            relativeWaypoint = await client.GetWaypoint(location.Value, context.GetCancellationToken())
                ?? throw new Exception("Unable to find relative waypoint.");
            marketplaces = marketplaces.OrderBy(w => w.Location.DistanceTo(relativeWaypoint.Location).Total);
        }

        await foreach (var market in marketplaces)
        {
            MarketplaceFormatter.WriteMarketplace(market, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}