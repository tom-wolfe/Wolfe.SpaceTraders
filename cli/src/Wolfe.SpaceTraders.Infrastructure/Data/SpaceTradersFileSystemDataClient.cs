using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Wolfe.SpaceTraders.Domain.Exploration;
using Wolfe.SpaceTraders.Domain.Marketplaces;
using Wolfe.SpaceTraders.Domain.Shipyards;
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
        _waypointsCollection = database.GetCollection<MongoWaypoint>(mongoOptions.Value.WaypointsCollection);
    }

    public Task AddMarketData(MarketData marketData, CancellationToken cancellationToken)
    {
        var file = Path.Combine(_options.MarketDataDirectory, $"{marketData.WaypointId.SystemId.Value}/{marketData.WaypointId.Value}.json");
        return AddItem(file, marketData, m => m.ToData(), cancellationToken);
    }

    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectory, $"{marketplace.SystemId.Value}/{marketplace.Id.Value}.json");
        return AddItem(file, marketplace, m => m.ToData(), cancellationToken);
    }

    public Task AddShipyard(Shipyard shipyard, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.ShipyardsDirectory, $"{shipyard.SystemId.Value}/{shipyard.Id.Value}.json");
        return AddItem(file, shipyard, s => s.ToData(), cancellationToken);
    }

    public Task AddSystem(StarSystem system, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.SystemsDirectory, $"{system.Id.Value}.json");
        return AddItem(file, system, s => s.ToData(), cancellationToken);
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

    public Task<DataItemResponse<Marketplace>?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectory, $"{marketplaceId.SystemId.Value}/{marketplaceId.Value}.json");
        return GetItem<Marketplace, DataMarketplace>(file, m => m.ToDomain(), cancellationToken);
    }

    public IAsyncEnumerable<DataItemResponse<Marketplace>>? GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectory, $"{systemId.Value}");
        return GetList<Marketplace, DataMarketplace>(file, m => m.ToDomain(), w => w.SystemId == systemId, cancellationToken);
    }

    public Task<DataItemResponse<Shipyard>?> GetShipyard(WaypointId shipyardId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.ShipyardsDirectory, $"{shipyardId.SystemId.Value}/{shipyardId.Value}.json");
        return GetItem<Shipyard, DataShipyard>(file, s => s.ToDomain(), cancellationToken);
    }

    public IAsyncEnumerable<DataItemResponse<Shipyard>>? GetShipyards(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.ShipyardsDirectory, $"{systemId.Value}");
        return GetList<Shipyard, DataShipyard>(file, s => s.ToDomain(), w => w.SystemId == systemId, cancellationToken);
    }

    public Task<DataItemResponse<StarSystem>?> GetSystem(SystemId systemId, CancellationToken cancellationToken)
    {
        var file = Path.Combine(_options.SystemsDirectory, $"{systemId.Value}.json");
        return GetItem<StarSystem, DataSystem>(file, s => s.ToDomain(), cancellationToken);
    }

    public IAsyncEnumerable<DataItemResponse<StarSystem>>? GetSystems(CancellationToken cancellationToken)
    {
        var file = _options.SystemsDirectory;
        return GetList<StarSystem, DataSystem>(file, s => s.ToDomain(), s => true, cancellationToken);
    }

    public async Task<Waypoint?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var results = await _waypointsCollection.FindAsync(w => w.Id == waypointId.Value, cancellationToken: cancellationToken);
        var mongoWaypoint = await results.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return mongoWaypoint?.ToDomain();
    }

    public async IAsyncEnumerable<Waypoint>? GetWaypoints(SystemId systemId, [EnumeratorCancellation] CancellationToken cancellationToken = default)
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