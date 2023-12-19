﻿using System.CommandLine;
using Wolfe.SpaceTraders.Core.Models;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoints;

internal static class WaypointsCommand
{
    public static readonly Argument<SystemSymbol> SystemIdArgument = new(
        name: "system-id",
        parse: r => new SystemSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))),
        description: "The ID of the system to list waypoints for."
    );

    public static readonly Option<WaypointType> TypeOption = new(
        aliases: ["-t", "--type"],
        parseArgument: r => new WaypointType(r.Tokens[0].Value),
        description: "The type of waypoint to filter by."
    )
    {
        Arity = ArgumentArity.ZeroOrOne,
        IsRequired = false
    };

    public static readonly Option<WaypointTraitSymbol[]> TraitsOption = new(
        aliases: ["-r", "--traits"],
        parseArgument: r => r.Tokens.Select(t => new WaypointTraitSymbol(t.Value)).ToArray(),
        description: "The traits to filter by."
    )
    {
        Arity = ArgumentArity.ZeroOrMore,
        IsRequired = false
    };

    public static readonly Option<WaypointSymbol?> DistanceOption = new(
        aliases: ["-d", "--distance"],
        parseArgument: r => new WaypointSymbol(r.Tokens[0].Value),
        description: "The distance to show."
    )
    {
        Arity = ArgumentArity.ZeroOrMore,
        IsRequired = false
    };

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "waypoints",
            description: "Displays the waypoints in the given system."
        );
        command.AddArgument(SystemIdArgument);
        command.AddOption(TypeOption);
        command.AddOption(TraitsOption);
        command.AddOption(DistanceOption);
        command.SetHandler(context => services.GetRequiredService<WaypointsCommandHandler>().InvokeAsync(context));

        return command;
    }
}