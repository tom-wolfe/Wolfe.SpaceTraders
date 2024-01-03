namespace Wolfe.SpaceTraders.Domain.Missions;

public interface IMissionLogProvider
{
    public IMissionLog CreateLog(MissionId missionId);
}
