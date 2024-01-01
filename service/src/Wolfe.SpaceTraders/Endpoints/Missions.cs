using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Endpoints;

public static class Missions
{
    public static WebApplication MapMissionEndpoints(this WebApplication app)
    {
        var missionsGroup = app.MapGroup("/missions");

        missionsGroup.MapGet("/", (IMissionService missionService, CancellationToken cancellationToken = default) => missionService.GetMissions(cancellationToken));
        missionsGroup.MapPost("/probe", async (IFleetService fleetService, IMissionService missionService, CreateProbeMissionRequest request, CancellationToken cancellationToken = default) =>
        {
            var ship = await fleetService.GetShip(request.ShipId, cancellationToken);
            if (ship == null) { return Results.NotFound(); }

            var mission = missionService.CreateProbeMission(ship);
            await mission.Start(cancellationToken);

            return Results.Accepted();
        });

        return app;
    }
}

public record CreateProbeMissionRequest(ShipId ShipId);