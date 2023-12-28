using Humanizer;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Domain.Contracts;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ContractFormatter
{
    public static void WriteContract(Contract contract)
    {
        Console.WriteLine($"~ {contract.Id.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"  Type: {contract.Type.Value.Color(ConsoleColors.Category)}");
        Console.WriteLine($"  Faction: {contract.FactionId.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"  Accepted?: {contract.Accepted.Humanize()}");
        Console.WriteLine($"  Fulfilled?: {contract.Fulfilled.Humanize()}");
        if (!contract.Accepted)
        {
            Console.WriteLine($"  Accept Deadline: {contract.DeadlineToAccept.Humanize()}");
        }

        Console.WriteLine($"  Terms:");
        Console.WriteLine($"  - Deadline: {contract.Terms.Deadline.Humanize()}");
        var payment = $"{contract.Terms.Payment.OnAccepted}/{contract.Terms.Payment.OnFulfilled} ({(contract.Terms.Payment.OnAccepted + contract.Terms.Payment.OnFulfilled)})".Color(ConsoleColors.Currency);
        Console.WriteLine($"  - Payment: {payment}");
        Console.WriteLine($"  - Deliver: ");
        foreach (var deliver in contract.Terms.Items)
        {
            Console.WriteLine($"    * Trade: {deliver.ItemId.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"      - Destination: {deliver.DestinationId.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"      - Quantity: {deliver.QuantityFulfilled}/{deliver.QuantityRequired}");
            if (deliver != contract.Terms.Items.Last()) { Console.WriteLine(); }
        }
    }
}