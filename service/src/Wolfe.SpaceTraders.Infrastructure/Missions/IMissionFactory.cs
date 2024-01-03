using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Infrastructure.Missions.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal interface IMissionFactory
{
    public IMission CreateProbeMission(Ship ship);
    public Task<IMission> Rehydrate(MongoMission mongoMission, CancellationToken cancellationToken = default);
}