namespace Wolfe.SpaceTraders.Domain.Missions.Scheduling;

public delegate Task MissionLoopDelegate(CancellationToken cancellationToken = default);
