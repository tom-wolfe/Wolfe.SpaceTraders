using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Shipyards;
using Wolfe.SpaceTraders.Infrastructure.Api;
using Wolfe.SpaceTraders.Infrastructure.Data.Mapping;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;
using Wolfe.SpaceTraders.Infrastructure.Mongo;
using Wolfe.SpaceTraders.Infrastructure.Mongo.Extensions;
using Wolfe.SpaceTraders.Infrastructure.Mongo.Mapping;
using Wolfe.SpaceTraders.Infrastructure.Mongo.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal class SpaceTradersFileSystemDataClient : ISpaceTradersDataClient
{
    private static readonly ReplaceOptions InsertOrUpdate = new() { IsUpsert = true };
    private readonly SpaceTradersDataOptions _options;
    private readonly IMongoCollection<MongoMarketplace> _marketplacesCollection;
    private readonly IMongoCollection<MongoShipyard> _shipyardsCollection;
    private readonly IMongoCollection<MongoSystem> _systemsCollection;
    private readonly IMongoCollection<MongoWaypoint> _waypointsCollection;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public SpaceTradersFileSystemDataClient(IOptions<SpaceTradersDataOptions> options, IOptions<MongoOptions> mongoOptions, IMongoClient mongoClient)
    {
        _options = options.Value;
        var database = mongoClient.GetDatabase(mongoOptions.Value.Database);
        _marketplacesCollection = database.GetCollection<MongoMarketplace>(mongoOptions.Value.MarketplacesCollection);
        _shipyardsCollection = database.GetCollection<MongoShipyard>(mongoOptions.Value.ShipyardsCollection);
        _systemsCollection = database.GetCollection<MongoSystem>(mongoOptions.Value.SystemsCollection);
        _waypointsCollection = database.GetCollection<MongoWaypoint>(mongoOptions.Value.WaypointsCollection);
    }

    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken)
    {
        var file = Path.Combine(_options.MarketDataDirectory, $"{marketData.WaypointId.SystemId.Value}/{marketData.WaypointId.Value}.json");
        return AddItem(file, marketData, m => m.ToData(), cancellationToken);
    }

    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default)
    {
        var mongoMarketplace = marketplace.ToMongo();
        return _marketplacesCollection.ReplaceOneAsync(x => x.Id == mongoMarketplace.Id, mongoMarketplace, InsertOrUpdate, cancellationToken);
    }

    public Task AddShipyard(Shipyard shipyard, CancellationToken cancellationToken = default)
    {
        var mongoShipyard = shipyard.ToMongo();
        return _shipyardsCollection.ReplaceOneAsync(x => x.Id == mongoShipyard.Id, mongoShipyard, InsertOrUpdate, cancellationToken);
    }

    public Task AddSystem(StarSystem system, CancellationToken cancellationToken = default)
    {
        var mongoSystem = system.ToMongo();
        return _systemsCollection.ReplaceOneAsync(x => x.Id == mongoSystem.Id, mongoSystem, InsertOrUpdate, cancellationToken);
    }

    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default)
    {
        var mongoWaypoint = waypoint.ToMongo();
        return _waypointsCollection.ReplaceOneAsync(x => x.Id == mongoWaypoint.Id, mongoWaypoint, InsertOrUpdate, cancellationToken);
    }

    public async Task<string?> GetAccessToken(CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.AccessTokensDirectory, "token.json");
        var data = await ReadItem<DataItem<DataAccessToken>>(file, cancellationToken);
        return data?.Item.Token;
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

    public async Task<StarSystem?> GetSystem(SystemId systemId, CancellationToken cancellationToken)
    {
        var results = await _systemsCollection.FindAsync(s => s.Id == systemId.Value, cancellationToken: cancellationToken);
        var mongoSystem = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoSystem?.ToDomain();
    }

    public async IAsyncEnumerable<StarSystem> GetSystems([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var query = await _systemsCollection.FindAsync(_ => true, cancellationToken: cancellationToken);
        var results = query.ToAsyncEnumerable(cancellationToken: cancellationToken);
        await foreach (var result in results)
        {
            yield return result.ToDomain();
        }
    }

    public async Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var results = await _waypointsCollection.FindAsync(w => w.Id == waypointId.Value, cancellationToken: cancellationToken);
        var mongoWaypoint = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoWaypoint?.ToDomain();
    }

    public async IAsyncEnumerable<Waypoint> GetWaypoints(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var query = await _waypointsCollection.FindAsync(w => w.SystemId == systemId.Value, cancellationToken: cancellationToken);
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

    private IAsyncEnumerable<DataItemResponse<TDomain>>? GetList<TDomain, TData>(
        string directory,
        Func<TData, TDomain> map,
        Func<TDomain, bool> filter,
        CancellationToken cancellationToken = default
    )
    {
        return Directory.Exists(directory) ? Yield(cancellationToken) : null;
        async IAsyncEnumerable<DataItemResponse<TDomain>> Yield([EnumeratorCancellation] CancellationToken ct)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                var item = await GetItem(file, map, ct)
                    ?? throw new Exception($"Error loading file: {file}.");
                if (filter(item.Item))
                {
                    yield return item;
                }
            }
        }
    }

    public Task SetAccessToken(string token, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.AccessTokensDirectory, "token.json");
        var data = new DataItem<DataAccessToken>
        {
            RetrievedAt = DateTimeOffset.UtcNow,
            Item = new DataAccessToken
            {
                Token = token
            }
        };
        return WriteItem(file, data, cancellationToken);
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