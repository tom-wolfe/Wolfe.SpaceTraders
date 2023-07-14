using Humanizer;
using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Contracts;

internal class ContractsCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ContractsCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(ContractsCommand.IdArgument);
        return id == null ? ListContracts(context) : GetContract(context, id);
    }

    private async Task<int> GetContract(InvocationContext context, string id)
    {
        var response = await _client.GetContract(id, context.GetCancellationToken());

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error getting contract: {response.StatusCode}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }

        var contract = response.Content!.Data;

        Console.WriteLine($"ID: {contract.Id.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"Type: {contract.Type.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Faction: {contract.FactionSymbol.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Accepted?: {contract.Accepted.Humanize()}");
        Console.WriteLine($"Fulfilled?: {contract.Fulfilled.Humanize()}");
        if (!contract.Accepted)
        {
            Console.WriteLine($"Accept Deadline: {contract.DeadlineToAccept.Humanize()}");
        }
        Console.WriteLine($"Terms:");
        Console.WriteLine($" - Deadline: {contract.Terms.Deadline.Humanize()}");
        Console.WriteLine($" - Payment: {contract.Terms.Payment.OnAccepted.ToMetric()}/{contract.Terms.Payment.OnFulfilled.ToMetric()} ({(contract.Terms.Payment.OnAccepted + contract.Terms.Payment.OnFulfilled).ToMetric()} total)");
        Console.WriteLine($" - Deliver: ");
        foreach (var deliver in contract.Terms.Deliver)
        {
            Console.WriteLine($"   - Trade: {deliver.TradeSymbol.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"   - Destination: {deliver.DestinationSymbol.Color(ConsoleColors.Code)}");
            Console.WriteLine($"   - Units: {deliver.UnitsFulfilled}/{deliver.UnitsRequired}");
            if (deliver != contract.Terms.Deliver.Last())
            {
                Console.WriteLine();
            }
        }

        return ExitCodes.Success;
    }

    private async Task<int> ListContracts(InvocationContext context)
    {
        var response = await _client.GetContracts(10, 1, context.GetCancellationToken());
        var contracts = response.Content!.Data.ToList();
        foreach (var contract in contracts)
        {
            Console.WriteLine($"ID: {contract.Id.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {contract.Type.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"Accepted?: {contract.Accepted.Humanize()}");
            Console.WriteLine($"Fulfilled?: {contract.Fulfilled.Humanize()}");
            if (!contract.Accepted)
            {
                Console.WriteLine($"Accept Deadline: {contract.DeadlineToAccept.Humanize()}");
            }

            if (contract != contracts.Last())
            {
                Console.WriteLine();
            }
        }
        return ExitCodes.Success;
    }
}