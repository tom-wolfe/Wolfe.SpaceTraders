using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Infrastructure.Data;
using Wolfe.SpaceTraders.Infrastructure.Data.Mapping;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;
using Wolfe.SpaceTraders.Infrastructure.Marketplaces.Models;
using Wolfe.SpaceTraders.Infrastructure.Mongo;

namespace Wolfe.SpaceTraders.Infrastructure.Marketplaces;

internal class MongoMarketplaceStore : IMarketplaceStore
{
    private readonly SpaceTradersDataOptions _options;
    private readonly IMongoCollection<MongoMarketplace> _marketplacesCollection;

    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public MongoMarketplaceStore(IOptions<SpaceTradersDataOptions> options, IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient)
    {
        _options = options.Value;
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _marketplacesCollection = database.GetCollection<MongoMarketplace>(mongoOptions.Value.MarketplacesCollection);
    }

    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken)
    {
        var file = Path.Combine(_options.MarketDataDirectory, $"{marketData.WaypointId.SystemId.Value}/{marketData.WaypointId.Value}.json");
        return AddItem(file, marketData, m => m.ToData(), cancellationToken);
    }

    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default)
    {
        var mongoMarketplace = marketplace.ToMongo();
        return _marketplacesCollection.ReplaceOneAsync(x => x.Id == mongoMarketplace.Id, mongoMarketplace, MongoHelpers.InsertOrUpdate, cancellationToken);
    }

    public Task<DataItemResponse<MarketData>?> GetMarketData(WaypointId marketplaceId, CancellationToken cancellationToken)
    {
        var file = Path.Combine(_options.MarketDataDirectory, $"{marketplaceId.SystemId.Value}/{marketplaceId.Value}.json");
        return GetItem<MarketData, DataMarketData>(file, m => m.ToDomain(), cancellationToken);
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

    private Task AddItem<TDomain, TData>(string file, TDomain item, Func<TDomain, TData> map, CancellationToken cancellationToken = default)
    {
        var data = new DataItem<TData>
        {
            RetrievedAt = DateTimeOffset.UtcNow,
            Item = map(item)
        };
        return WriteItem(file, data, cancellationToken);
    }

    private async Task<DataItemResponse<TDomain>?> GetItem<TDomain, TData>(string file, Func<TData, TDomain> map, CancellationToken cancellationToken = default)
    {
        var data = await ReadItem<DataItem<TData>>(file, cancellationToken);
        if (data == null) { return null; }

        return new DataItemResponse<TDomain>
        {
            RetrievedAt = data.RetrievedAt,
            Item = map(data.Item)
        };
    }

    private async Task<T?> ReadItem<T>(string path, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(path)) { return default; }
        await using var fileStream = File.OpenRead(path);

        var data = await JsonSerializer.DeserializeAsync<T>(fileStream, _jsonOptions, cancellationToken)
                   ?? throw new Exception("Unable to deserialize data.");
        return data;
    }

    private async Task WriteItem<T>(string path, T data, CancellationToken cancellationToken = default)
    {
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, data, _jsonOptions, cancellationToken);
        stream.Position = 0;

        new FileInfo(path).Directory?.Create();
        await using var fileStream = File.Create(path);
        await stream.CopyToAsync(fileStream, cancellationToken);
    }
}