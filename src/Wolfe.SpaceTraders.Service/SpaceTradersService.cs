namespace Wolfe.SpaceTraders.Service;

public class SpaceTradersService : ISpaceTradersService
{
    private readonly ISpaceTradersClient _client;

    public SpaceTradersService(ISpaceTradersClient client)
    {
        _client = client;
    }

    public async Task BuyFirstShip(CancellationToken cancellationToken = default)
    {
        var agent = await _client.GetAgent(cancellationToken);
        agent.Headquarters
    }
}