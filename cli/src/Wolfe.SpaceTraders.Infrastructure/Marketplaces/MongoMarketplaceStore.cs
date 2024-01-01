using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Marketplaces.Models;
using Wolfe.SpaceTraders.Infrastructure.Mongo;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal class MongoMarketplaceStore : IMarketplaceStore
{
    private readonly IMongoCollection<MongoMarketplace> _marketplacesCollection;
    private readonly IMongoCollection<MongoMarketData> _marketDataCollection;

    public MongoMarketplaceStore(IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _marketplacesCollection = database.GetCollection<MongoMarketplace>(mongoOptions.Value.MarketplacesCollection);
        _marketDataCollection = database.GetCollection<MongoMarketData>(mongoOptions.Value.MarketDataCollection);
    }

    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken)
    {
        var mongoMarketData = marketData.ToMongo();
        return _marketDataCollection.ReplaceOneAsync(x => x.WaypointId == mongoMarketData.WaypointId, mongoMarketData, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default)
    {
        var mongoMarketplace = marketplace.ToMongo();
        return _marketplacesCollection.ReplaceOneAsync(x => x.Id == mongoMarketplace.Id, mongoMarketplace, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public async Task<MarketData?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken)
    {
        var results = await _marketDataCollection.FindAsync(s => s.WaypointId == marketplaceId.Value, cancellationToken: cancellationToken);
        var mongoMarketData = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoMarketData?.ToDomain();
    }

    public async Task<Marketplace?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var results = await _marketplacesCollection.FindAsync(s => s.Id == marketplaceId.Value, cancellationToken: cancellationToken);
        var mongoMarketplace = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoMarketplace?.ToDomain();
    }

    public async IAsyncEnumerable<Marketplace> GetMarketplaces(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var query = await _marketplacesCollection.FindAsync(w => w.SystemId == systemId.Value, cancellationToken: cancellationToken);
        var results = query.ToAsyncEnumerable(cancellationToken: cancellationToken);
        await foreach (var result in results)
        {
            yield return result.ToDomain();
        }
    }

    public Task Clear(CancellationToken cancellationToken = default)
    {
        return Task.WhenAll(
            _marketDataCollection.DeleteManyAsync(_ => true, cancellationToken),
            _marketplacesCollection.DeleteManyAsync(_ => true, cancellationToken)
        );
    }
}