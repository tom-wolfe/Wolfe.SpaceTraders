using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal interface IMissionLogFactory
{
    public IMissionLog CreateLog(MissionId missionId);
}
