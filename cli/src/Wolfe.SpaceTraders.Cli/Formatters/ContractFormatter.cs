using Wolfe.SpaceTraders.Domain.Contracts;

namespace Wolfe.SpaceTraders.Cli.Formatters;

internal static class ContractFormatter
{
    public static void WriteContract(Contract contract)
    {
        ConsoleHelpers.WriteLineFormatted($"~ {contract.Id}");
        ConsoleHelpers.WriteLineFormatted($"  Type: {contract.Type}");
        ConsoleHelpers.WriteLineFormatted($"  Faction: {contract.FactionId}");
        ConsoleHelpers.WriteLineFormatted($"  Accepted?: {contract.Accepted}");
        ConsoleHelpers.WriteLineFormatted($"  Fulfilled?: {contract.Fulfilled}");
        if (!contract.Accepted)
        {
            ConsoleHelpers.WriteLineFormatted($"  Accept Deadline: {contract.DeadlineToAccept}");
        }

        ConsoleHelpers.WriteLineFormatted($"  Terms:");
        ConsoleHelpers.WriteLineFormatted($"  - Deadline: {contract.Terms.Deadline}");
        ConsoleHelpers.WriteLineFormatted($"  - Payment: {contract.Terms.Payment.OnAccepted}/{contract.Terms.Payment.OnFulfilled} ({contract.Terms.Payment.OnAccepted + contract.Terms.Payment.OnFulfilled})");
        ConsoleHelpers.WriteLineFormatted($"  - Deliver: ");
        foreach (var deliver in contract.Terms.Items)
        {
            ConsoleHelpers.WriteLineFormatted($"    * Trade: {deliver.ItemId}");
            ConsoleHelpers.WriteLineFormatted($"      - Destination: {deliver.DestinationId}");
            ConsoleHelpers.WriteLineFormatted($"      - Quantity: {deliver.QuantityFulfilled}/{deliver.QuantityRequired}");
        }
    }
}