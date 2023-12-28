using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

public class ProcurementContractMission(Ship ship, Contract contract)
{
    public async Task Execute(CancellationToken cancellationToken = default)
    {
        while (!contract.IsComplete())
        {
            await ClearUnnecessaryItems();
            await ExtractContractItems();
            await DeliverContractItems();
        }
        await TurnInContract();
    }

    private async Task DeliverContractItems()
    {
        Thread.Sleep(1000);
    }

    private async Task TurnInContract()
    {
        Thread.Sleep(1000);
    }

    private async Task ClearUnnecessaryItems()
    {
        var contractItems = contract.GetOutstandingItems().Select(c => c.ItemId);
        var itemsToRemove = ship.Cargo.Items.ExceptBy(contractItems, i => i.Id);

        foreach (var item in itemsToRemove)
        {
            await ship.Jettison(item.Id, item.Quantity);
        }
    }

    private async Task ExtractContractItems()
    {
        Thread.Sleep(1000);
    }
}
