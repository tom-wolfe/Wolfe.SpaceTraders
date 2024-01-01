using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Mongo;
using Wolfe.SpaceTraders.Infrastructure.Shipyards.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Shipyards;

internal class MongoShipyardStore : IShipyardStore
{
    private readonly IMongoCollection<MongoShipyard> _shipyardsCollection;

    public MongoShipyardStore(IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _shipyardsCollection = database.GetCollection<MongoShipyard>(mongoOptions.Value.ShipyardsCollection);
    }

    public Task AddShipyard(Shipyard shipyard, CancellationToken cancellationToken = default)
    {
        var mongoShipyard = shipyard.ToMongo();
        return _shipyardsCollection.ReplaceOneAsync(x => x.Id == mongoShipyard.Id, mongoShipyard, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public async Task<Shipyard?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default)
    {
        var results = await _shipyardsCollection.FindAsync(s => s.Id == shipyardId.Value, cancellationToken: cancellationToken);
        var mongoShipyard = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoShipyard?.ToDomain();
    }

    public async IAsyncEnumerable<Shipyard> GetShipyards(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var query = await _shipyardsCollection.FindAsync(w => w.SystemId == systemId.Value, cancellationToken: cancellationToken);
        var results = query.ToAsyncEnumerable(cancellationToken: cancellationToken);
        await foreach (var result in results)
        {
            yield return result.ToDomain();
        }
    }

    public Task Clear(CancellationToken cancellationToken = default)
    {
        return _shipyardsCollection.DeleteManyAsync(_ => true, cancellationToken);
    }
}