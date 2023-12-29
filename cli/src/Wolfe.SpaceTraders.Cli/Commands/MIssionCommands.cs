using Cocona;
using Microsoft.Extensions.Hosting;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;
using Wolfe.SpaceTraders.Service.Contracts;
using Wolfe.SpaceTraders.Service.Missions;
using Wolfe.SpaceTraders.Service.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands;

internal class MissionCommands(
    IMissionService missionService,
    IShipService shipService,
    IContractService contractService,
    IHostApplicationLifetime host
)
{
    public async Task<int> Contract([Argument] ShipId shipId, [Argument] ContractId contractId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship '{shipId}' not found.");
        var contract = await contractService.GetContract(contractId, host.ApplicationStopping) ?? throw new Exception($"Contract '{contractId}' not found.");

        var mission = missionService.CreateContractMission(ship, contract);
        await mission.Execute(host.ApplicationStopping);

        return ExitCodes.Success;
    }

    public async Task<int> Probe([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship '{shipId}' not found.");

        var mission = missionService.CreateProbeMission(ship);
        await mission.Execute(host.ApplicationStopping);

        return ExitCodes.Success;
    }

    public async Task<int> Trading([Argument] ShipId shipId)
    {
        var ship = await shipService.GetShip(shipId, host.ApplicationStopping) ?? throw new Exception($"Ship '{shipId}' not found.");

        var mission = missionService.CreateTradeMission(ship);
        await mission.Execute(host.ApplicationStopping);

        return ExitCodes.Success;
    }
}
