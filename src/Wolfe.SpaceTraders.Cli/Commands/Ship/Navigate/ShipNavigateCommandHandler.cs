using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;
using Wolfe.SpaceTraders.Service.Requests;

namespace Wolfe.SpaceTraders.Cli.Commands.Ship.Navigate;

internal class ShipNavigateCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ShipNavigateCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var shipId = context.BindingContext.ParseResult.GetValueForArgument(ShipNavigateCommand.ShipIdArgument);
        var waypointId = context.BindingContext.ParseResult.GetValueForArgument(ShipNavigateCommand.WaypointIdArgument);
        var request = new ShipNavigateRequest
        {
            WaypointSymbol = waypointId
        };
        await _client.ShipNavigate(shipId, request, context.GetCancellationToken());

        Console.WriteLine("Your ship is now in transit.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}