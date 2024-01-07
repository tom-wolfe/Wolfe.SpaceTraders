using Microsoft.AspNetCore.Mvc;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.General;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Exploration
{
    public static WebApplication MapExplorationEndpoints(this WebApplication app)
    {
        var systemGroup = app.MapGroup("/systems");

        systemGroup.MapGet("/", (IExplorationService explorationService, CancellationToken cancellationToken = default) => explorationService.GetSystems(cancellationToken));
        systemGroup.MapGet("/{systemId}", async (IExplorationService explorationService, SystemId systemId, CancellationToken cancellationToken = default) =>
        {
            var system = await explorationService.GetSystem(systemId, cancellationToken);
            return system == null ? Results.NotFound() : Results.Ok(system);
        });
        systemGroup.MapGet("{systemId}/waypoints", async (IExplorationService explorationService, SystemId systemId, [FromQuery] WaypointType? type, [FromQuery] WaypointTraitId[]? traits, [FromQuery] WaypointId? nearestTo, CancellationToken cancellationToken = default) =>
        {
            var waypoints = explorationService.GetWaypoints(systemId, cancellationToken);

            if (type != null)
            {
                waypoints = waypoints.Where(w => w.Type == type.Value);
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

            return waypoints;
        });

        var waypointGroup = app.MapGroup("/waypoints");
        waypointGroup.MapGet("/{waypointId}", async (IExplorationService explorationService, WaypointId waypointId, CancellationToken cancellationToken = default) =>
        {
            var waypoint = await explorationService.GetWaypoint(waypointId, cancellationToken);
            return waypoint == null ? Results.NotFound() : Results.Ok(waypoint);
        });

        app.MapGet("/route", async (IWayfinderService wayfinder, WaypointId from, WaypointId to, uint max, CancellationToken cancellationToken = default) =>
        {
            var maxDistance = new Distance(max, 0);
            var path = await wayfinder.FindPath(from, to, maxDistance, cancellationToken);
            return path;
        });

        return app;
    }
}