using Cocona;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class MarketplaceCommands(IExplorationService explorationService)
{
    public async Task<int> Get([Argument] WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var marketplace = await explorationService.GetMarketplace(marketplaceId, cancellationToken) ?? throw new Exception($"Marketplace '{marketplaceId}' not found.");
        MarketplaceFormatter.WriteMarketplace(marketplace);

        return ExitCodes.Success;
    }

    public async Task<int> List([Argument] SystemId systemId, [Option] ItemId? buying, [Option] ItemId? selling, [Option] WaypointId? nearestTo, CancellationToken cancellationToken = default)
    {
        var marketplaces = explorationService.GetMarketplaces(systemId, cancellationToken);

        if (selling != null)
        {
            marketplaces = marketplaces.Where(m => m.IsSelling(selling.Value));
        }

        if (buying != null)
        {
            marketplaces = marketplaces.Where(m => m.IsBuying(buying.Value));
        }

        Waypoint? relativeWaypoint = null;
        if (nearestTo != null)
        {
            relativeWaypoint = await explorationService.GetWaypoint(nearestTo.Value, cancellationToken) ?? throw new Exception("Unable to find relative waypoint.");
            marketplaces = marketplaces.OrderBy(w => w.Location.DistanceTo(relativeWaypoint.Location).Total);
        }

        await foreach (var marketplace in marketplaces)
        {
            MarketplaceFormatter.WriteMarketplace(marketplace, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}
