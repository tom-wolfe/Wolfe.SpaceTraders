using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions.Logs;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

internal class MissionService(
    IMissionLogFactory logFactory,
    IMarketplaceService marketplaceService,
    IMarketPriorityService marketPriorityService,
    IWayfinderService wayfinderService
) : IMissionService
{
    public IMission CreateContractMission(Ship ship, Contract contract)
    {
        var missionId = MissionId.Generate();
        var log = logFactory.CreateLog(missionId);

        if (contract.Type == ContractType.Procurement)
        {
            return new ProcurementContractMission(missionId, log, ship, contract);
        }
        throw new NotImplementedException();
    }

    public IMission CreateProbeMission(Ship ship)
    {
        var missionId = MissionId.Generate();
        return new ProbeMission(missionId, logFactory.CreateLog(missionId), ship, marketplaceService, marketPriorityService);
    }

    public IMission CreateTradeMission(Ship ship)
    {
        var missionId = MissionId.Generate();

        return new TradingMission(missionId, logFactory.CreateLog(missionId), ship, wayfinderService);
    }
}
