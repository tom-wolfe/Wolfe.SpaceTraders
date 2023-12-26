﻿using System.CommandLine.Invocation;
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
            if (selling != null && !market.Exports.Any(e => e.Symbol == selling.Value))
            {
                continue;
            }

            if (buying != null && !market.Imports.Any(i => i.Symbol == buying.Value))
            {
                continue;
            }

            MarketplaceFormatter.WriteMarketplace(market, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}