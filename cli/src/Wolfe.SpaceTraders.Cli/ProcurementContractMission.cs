using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli;

internal class ProcurementContractMission(Ship ship, Contract contract)
{
    public async Task Execute()
    {
        while (!contract.IsComplete())
        {
            await ClearUnnecessaryCargo();
            await AcquireContractGoods();
            await DeliverContractGoods();
        }
        await TurnInContract();
    }

    private async Task DeliverContractGoods()
    {
        Thread.Sleep(1000);
    }

    private async Task TurnInContract()
    {
        Thread.Sleep(1000);
    }

    private async Task ClearUnnecessaryCargo()
    {
        var contractGoods = contract.GetOutstandingItems().Select(c => c.TradeId);
        var itemsToRemove = ship.Cargo.Inventory
            .ExceptBy(contractGoods, y => y.Id);

        // TODO: Sell what we can, and dump the rest.
        //foreach (var item in itemsToRemove)
        //{
        //    await ship.Cargo.Jettison(item.Id, item.Quantity);
        //}
    }

    private async Task AcquireContractGoods()
    {
        Thread.Sleep(1000);
    }
}
