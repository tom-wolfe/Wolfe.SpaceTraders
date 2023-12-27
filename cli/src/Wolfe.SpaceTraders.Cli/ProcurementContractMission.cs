using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli;

internal class ProcurementContractMission(Ship ship, Contract contract)
{
    public async Task Execute()
    {
        while (!contract.IsComplete())
        {
            await ClearUnnecessaryItems();
            await AcquireContractItems();
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
        var itemsToRemove = ship.Cargo.Items
            .ExceptBy(contractItems, i => i.Id);

        // TODO: Sell what we can, and dump the rest.
        foreach (var item in itemsToRemove)
        {
            await ship.Jettison(item.Id, item.Quantity);
        }
    }

    private async Task AcquireContractItems()
    {
        Thread.Sleep(1000);
    }
}
