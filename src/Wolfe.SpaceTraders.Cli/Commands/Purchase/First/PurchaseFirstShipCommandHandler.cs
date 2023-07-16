using System.CommandLine.Invocation;
using Wolfe.SpaceTraders.Cli.Extensions;
using Wolfe.SpaceTraders.Service;

namespace Wolfe.SpaceTraders.Cli.Commands.Purchase.First;

internal class PurchaseFirstShipCommandHandler : CommandHandler
{
    private readonly ISpaceTradersService _service;

    public PurchaseFirstShipCommandHandler(ISpaceTradersService service)
    {
        _service = service;
    }

    public override async Task<int> InvokeAsync(InvocationContext context)
    {
        await _service.PurchaseFirstShip();
        Console.WriteLine("Purchased first ship successfully.".Color(ConsoleColors.Success));
        return ExitCodes.Success;
    }
}