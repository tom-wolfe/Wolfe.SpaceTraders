﻿using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Contract;

internal class ContractCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(ContractCommand.ContractIdArgument);

        try
        {
            var contract = await client.GetContract(id, context.GetCancellationToken())
                ?? throw new Exception($"Contract '{id}' not found.");

            Console.WriteLine($"ID: {contract.Id.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {contract.Type.Value.Color(ConsoleColors.Category)}");
            Console.WriteLine($"Faction: {contract.FactionSymbol.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"Accepted?: {contract.Accepted.Humanize()}");
            Console.WriteLine($"Fulfilled?: {contract.Fulfilled.Humanize()}");
            if (!contract.Accepted)
            {
                Console.WriteLine($"Accept Deadline: {contract.DeadlineToAccept.Humanize()}");
            }

            Console.WriteLine($"Terms:");
            Console.WriteLine($" - Deadline: {contract.Terms.Deadline.Humanize()}");
            var payment = $"{contract.Terms.Payment.OnAccepted}/{contract.Terms.Payment.OnFulfilled} ({(contract.Terms.Payment.OnAccepted + contract.Terms.Payment.OnFulfilled)})".Color(ConsoleColors.Currency);
            Console.WriteLine($" - Payment: {payment}");
            Console.WriteLine($" - Deliver: ");
            foreach (var deliver in contract.Terms.Deliver)
            {
                Console.WriteLine($"   - Trade: {deliver.TradeSymbol.Value.Color(ConsoleColors.Code)}");
                Console.WriteLine($"   - Destination: {deliver.DestinationSymbol.Value.Color(ConsoleColors.Code)}");
                Console.WriteLine($"   - Units: {deliver.UnitsFulfilled}/{deliver.UnitsRequired}");
                if (deliver != contract.Terms.Deliver.Last()) { Console.WriteLine(); }
            }

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting contract: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}