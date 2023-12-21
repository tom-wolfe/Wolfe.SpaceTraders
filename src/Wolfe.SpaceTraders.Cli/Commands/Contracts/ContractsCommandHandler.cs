using Humanizer;
using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Contracts;

internal class ContractsCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ContractsCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override Task<int> InvokeAsync(InvocationContext context)
    {
        var contracts = _client
            .GetContracts(context.GetCancellationToken())
            .ToBlockingEnumerable(context.GetCancellationToken())
            .ToList();

        foreach (var contract in contracts)
        {
            Console.WriteLine($"ID: {contract.Id.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {contract.Type.Value.Color(ConsoleColors.Category)}");
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
        return Task.FromResult(ExitCodes.Success);
    }
}