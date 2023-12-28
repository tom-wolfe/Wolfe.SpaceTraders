using Cocona;
using Wolfe.SpaceTraders.Cli.Formatters;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class ExplorationCommands(IExplorationService explorationService)
{
    public async Task<int> Marketplaces([Argument] SystemId systemId, [Option] ItemId? buying, [Option] ItemId? selling, [Option] WaypointId? nearestTo, CancellationToken cancellationToken = default)
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

    public async Task<int> Shipyard([Argument] WaypointId shipyardId, CancellationToken cancellationToken = default)
    {
        var shipyard = await explorationService.GetShipyard(shipyardId, cancellationToken) ?? throw new Exception($"Shipyard '{shipyardId}' not found.");
        ShipyardFormatter.WriteShipyard(shipyard);

        return ExitCodes.Success;
    }

    public async Task<int> Shipyards([Argument] SystemId systemId, [Option] ShipType? selling, [Option] WaypointId? nearestTo, CancellationToken cancellationToken = default)
    {
        var shipyards = explorationService.GetShipyards(systemId, cancellationToken);

        if (selling != null)
        {
            shipyards = shipyards.Where(s => s.IsSelling(selling.Value));
        }

        Waypoint? relativeWaypoint = null;
        if (nearestTo != null)
        {
            relativeWaypoint = await explorationService.GetWaypoint(nearestTo.Value, cancellationToken) ?? throw new Exception("Unable to find relative waypoint.");
            shipyards = shipyards.OrderBy(w => w.Location.DistanceTo(relativeWaypoint.Location).Total);
        }

        await foreach (var shipyard in shipyards)
        {
            ShipyardFormatter.WriteShipyard(shipyard, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }

    public async Task<int> System([Argument] SystemId systemId, CancellationToken cancellationToken = default)
    {
        var system = await explorationService.GetSystem(systemId, cancellationToken) ?? throw new Exception($"System '{systemId}' not found.");
        SystemFormatter.WriteSystem(system);

        return ExitCodes.Success;
    }

    public async Task<int> Systems(CancellationToken cancellationToken = default)
    {
        var systems = explorationService.GetSystems(cancellationToken);

        await foreach (var system in systems)
        {
            SystemFormatter.WriteSystem(system);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }

    public async Task<int> Waypoint([Argument] WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var waypoint = await explorationService.GetWaypoint(waypointId, cancellationToken) ?? throw new Exception($"Waypoint '{waypointId}' not found.");
        WaypointFormatter.WriteWaypoint(waypoint);

        return ExitCodes.Success;
    }

    public async Task<int> Waypoints([Argument] SystemId systemId, [Option] WaypointType? type, [Option] WaypointTraitId[]? traits, [Option] WaypointId? nearestTo, CancellationToken cancellationToken = default)
    {
        var waypoints = explorationService.GetWaypoints(systemId, cancellationToken);

        if (type != null)
        {
            waypoints = waypoints.Where(w => w.Type == type);
        }

        if (traits?.Length > 0)
        {
            waypoints = waypoints.Where(w => w.HasAnyTrait(traits));
        }

        Waypoint? relativeWaypoint = null;
        if (nearestTo != null)
        {
            relativeWaypoint = await explorationService.GetWaypoint(nearestTo.Value, cancellationToken) ?? throw new Exception("Unable to find relative waypoint.");
            waypoints = waypoints.OrderBy(w => w.Location.DistanceTo(relativeWaypoint.Location).Total);
        }

        await foreach (var waypoint in waypoints)
        {
            WaypointFormatter.WriteWaypoint(waypoint, relativeWaypoint?.Location);
            Console.WriteLine();
        }
        return ExitCodes.Success;
    }
}
