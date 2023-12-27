using System.CommandLine;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;

namespace Wolfe.SpaceTraders.Cli.Commands.Markets;

internal static class MarketsCommand
{
    public static readonly Argument<SystemId> SystemIdArgument = new(
        name: "system-id",
        parse: r => new SystemId(string.Join(' ', r.Tokens.Select(t => t.Value))),
        description: "The ID of the system to list markets for."
    );

    public static readonly Option<ItemId?> BuyingOption = new(
        aliases: ["-b", "--buying"],
        parseArgument: r => new ItemId(r.Tokens[0].Value),
        description: "The items that the markets must buy."
    )
    {
        IsRequired = false
    };

    public static readonly Option<ItemId?> SellingOption = new(
        aliases: ["-s", "--selling"],
        parseArgument: r => new ItemId(r.Tokens[0].Value),
        description: "The items that the market must sell."
    )
    {
        IsRequired = false
    };

    public static readonly Option<WaypointId?> NearestToOption = new(
        aliases: ["-n", "--nearest-to"],
        parseArgument: r => new WaypointId(r.Tokens[0].Value),
        description: "The location to show distance relative to."
    )
    {
        IsRequired = false
    };

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "markets",
            description: "Displays the markets in the given system."
        );
        command.AddArgument(SystemIdArgument);
        command.AddOption(BuyingOption);
        command.AddOption(SellingOption);
        command.AddOption(NearestToOption);
        command.SetHandler(context => services.GetRequiredService<MarketsCommandHandler>().InvokeAsync(context));

        return command;
    }
}