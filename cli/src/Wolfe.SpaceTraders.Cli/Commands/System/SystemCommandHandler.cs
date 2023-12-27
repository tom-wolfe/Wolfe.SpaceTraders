using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.System;

internal class SystemCommandHandler(ISpaceTradersClient client) : CommandHandler
{
    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(SystemCommand.SystemIdArgument);
        try
        {
            var system = await client.GetSystem(id, context.GetCancellationToken())
                ?? throw new Exception($"System '{id}' not found.");

            Console.WriteLine($"ID: {system.Id.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Sector: {system.SectorId.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {system.Type.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"Location: {system.Location}");

            // TODO: List waypoints and factions

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting system: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}