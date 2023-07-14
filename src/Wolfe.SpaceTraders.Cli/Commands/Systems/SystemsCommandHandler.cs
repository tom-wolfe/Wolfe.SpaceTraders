using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Systems;

internal class SystemsCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public SystemsCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(SystemsCommand.IdArgument);
        return id == null ? ListSystems(context) : GetSystem(context, id);
    }

    private async Task<int> GetSystem(InvocationContext context, string id)
    {
        var response = await _client.GetSystem(id, context.GetCancellationToken());

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error getting system: {response.StatusCode}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }

        var system = response.Content!.Data;

        Console.WriteLine($"ID: {system.Symbol.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"Sector: {system.SectorSymbol.Value.Color(ConsoleColors.Id)}");
        Console.WriteLine($"Type: {system.Type.Value.Color(ConsoleColors.Code)}");
        Console.WriteLine($"Position: {system.X}, {system.Y}");

        // TODO: List waypoints and factions

        return ExitCodes.Success;
    }

    private async Task<int> ListSystems(InvocationContext context)
    {
        var response = await _client.GetSystems(10, 1, context.GetCancellationToken());
        var systems = response.Content!.Data.ToList();
        foreach (var system in systems)
        {
            Console.WriteLine($"ID: {system.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {system.Type.Value.Color(ConsoleColors.Code)}");

            if (system != systems.Last())
            {
                Console.WriteLine();
            }
        }
        return ExitCodes.Success;
    }
}