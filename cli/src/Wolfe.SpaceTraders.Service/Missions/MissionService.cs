using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Missions;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Service.Missions;

internal class MissionService(
    IMissionLogFactory logFactory,
    IMarketplaceService marketplaceService,
    IWayfinderService wayfinderService
) : IMissionService
{
    public IMission CreateContractMission(Ship ship, Contract contract)
    {
        if (contract.Type == ContractType.Procurement)
        {
            return new ProcurementContractMission(logFactory.CreateMissionLog(), ship, contract);
        }
        throw new NotImplementedException();
    }

    public IMission CreateProbeMission(Ship ship) => new ProbeMission(logFactory.CreateMissionLog(), ship, marketplaceService);
    public IMission CreateTradeMission(Ship ship) => new TradingMission(logFactory.CreateMissionLog(), ship, wayfinderService);
}