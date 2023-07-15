using System.CommandLine.Invocation;

namespace Wolfe.SpaceTraders.Commands.Ship;

internal class ShipCommandHandler : CommandHandler
{
    private readonly ISpaceTradersClient _client;

    public ShipCommandHandler(ISpaceTradersClient client)
    {
        _client = client;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        var id = context.BindingContext.ParseResult.GetValueForArgument(ShipCommand.ShipIdArgument);
        try
        {
            var ship = await _client.GetShip(id, context.GetCancellationToken());
            if (ship == null)
            {
                throw new Exception($"Ship '{id}' not found.");
            }

            Console.WriteLine($"ID: {ship.Symbol.Value.Color(ConsoleColors.Id)}");

            // TODO: List everything else.

            return ExitCodes.Success;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error getting system: {ex.Message}.".Color(ConsoleColors.Error));
            return ExitCodes.Error;
        }
    }
}