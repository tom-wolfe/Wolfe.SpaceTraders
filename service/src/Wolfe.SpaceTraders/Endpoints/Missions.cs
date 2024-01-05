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

            var mission = await missionService.CreateProbeMission(ship, cancellationToken);
            await mission.Start(cancellationToken);

            return Results.Accepted();
        });

        var missionGroup = missionsGroup.MapGroup("/{missionId}");
        missionGroup.MapGet("/", async (IMissionService missionService, MissionId missionId, CancellationToken cancellationToken = default) =>
        {
            var mission = await missionService.GetMission(missionId, cancellationToken);
            return mission == null ? Results.NotFound() : Results.Ok(mission);
        });
        missionGroup.MapPost("/start", async (IMissionService missionService, MissionId missionId, CancellationToken cancellationToken = default) =>
        {
            var mission = await missionService.GetMission(missionId, cancellationToken);
            if (mission == null) { return Results.NotFound(); }
            await mission.Start(cancellationToken);
            return Results.Accepted();
        });
        missionGroup.MapPost("/stop", async (IMissionService missionService, MissionId missionId, CancellationToken cancellationToken = default) =>
        {
            var mission = await missionService.GetMission(missionId, cancellationToken);
            if (mission == null) { return Results.NotFound(); }
            await mission.Stop(cancellationToken);
            return Results.Accepted();
        });

        return app;
    }
}

public record CreateProbeMissionRequest(ShipId ShipId);