using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Models.Systems;
using Wolfe.SpaceTraders.Domain.Models.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Commands.Waypoints;

internal static class WaypointsCommand
{
    public static readonly Argument<SystemSymbol> SystemIdArgument = new(
        name: "system-id",
        parse: r => new SystemSymbol(string.Join(' ', r.Tokens.Select(t => t.Value))),
        description: "The ID of the system to list waypoints for."
    );

    public static readonly Option<WaypointType?> TypeOption = new(
        aliases: ["-t", "--type"],
        parseArgument: r => new WaypointType(r.Tokens[0].Value),
        description: "The type of waypoint to filter by."
    )
    {
        Arity = ArgumentArity.ExactlyOne,
        IsRequired = false
    };

    public static readonly Option<WaypointTraitSymbol[]?> TraitsOption = new(
        aliases: ["-r", "--traits"],
        parseArgument: r => r.Tokens.Select(t => new WaypointTraitSymbol(t.Value)).ToArray(),
        description: "The traits to filter by."
    )
    {
        Arity = ArgumentArity.OneOrMore,
        IsRequired = false
    };

    public static readonly Option<WaypointSymbol?> NearestToOption = new(
        aliases: ["-n", "--nearest-to"],
        parseArgument: r => new WaypointSymbol(r.Tokens[0].Value),
        description: "The location to show distance relative to."
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
        command.AddOption(NearestToOption);
        command.SetHandler(context => services.GetRequiredService<WaypointsCommandHandler>().InvokeAsync(context));

        return command;
    }
}