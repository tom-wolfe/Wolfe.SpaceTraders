﻿using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.System;

internal class SystemCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public SystemCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(SystemCommand.SystemIdArgument);
        try
        {
            var system = await _client.GetSystem(id, context.GetCancellationToken());
            if (system == null)
            {
                throw new Exception($"System '{id}' not found.");
            }

            Console.WriteLine($"ID: {system.Symbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Sector: {system.SectorSymbol.Value.Color(ConsoleColors.Id)}");
            Console.WriteLine($"Type: {system.Type.Value.Color(ConsoleColors.Code)}");
            Console.WriteLine($"Position: {system.Location}");

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