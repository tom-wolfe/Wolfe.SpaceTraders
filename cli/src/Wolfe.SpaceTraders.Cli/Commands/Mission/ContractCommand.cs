using System.CommandLine;
using Wolfe.SpaceTraders.Cli.Commands.Contract.Accept;
using Wolfe.SpaceTraders.Domain.Contracts;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Cli.Commands.Mission;

internal static class MissionCommand
{
    public static readonly Argument<ShipId> ShipIdArgument = new(
        name: "ship-id",
        parse: r => new ShipId(r.Tokens[0].Value),
        description: "The ID of the ship to set on a mission."
    );

    public static readonly Argument<ContractId> ContractIdArgument = new(
        name: "contract-id",
        parse: r => new ContractId(r.Tokens[0].Value),
        description: "The ID of the contract to set the ship on a mission to complete."
    );

    public static Command CreateCommand(IServiceProvider services)
    {
        var command = new Command(
            name: "mission",
            description: "Sets a ship on a mission to complete a contractS."
        );
        command.AddArgument(ShipIdArgument);
        command.AddArgument(ContractIdArgument);
        command.SetHandler(context => services.GetRequiredService<MissionCommandHandler>().InvokeAsync(context));

        command.AddCommand(AcceptContractCommand.CreateCommand(services));

        return command;
    }
}