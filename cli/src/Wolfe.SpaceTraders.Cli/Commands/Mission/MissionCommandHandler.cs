using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Fleet;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Mission;

internal class MissionCommandHandler(ISpaceTradersClient client, IFleetClient fleetClient) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(MissionCommand.ShipIdArgument);
        var contractId = context.BindingContext.ParseResult.GetValueForArgument(MissionCommand.ContractIdArgument);

        try
        {
            var ship = await fleetClient.GetShip(shipId, context.GetCancellationToken())
                ?? throw new Exception($"Ship '{shipId}' not found.");

            var contract = await client.GetContract(contractId, context.GetCancellationToken())
                ?? throw new Exception($"Contract '{contractId}' not found.");

            var mission = new ProcurementContractMission(ship, contract);
            await mission.Execute();

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error completing mission: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}