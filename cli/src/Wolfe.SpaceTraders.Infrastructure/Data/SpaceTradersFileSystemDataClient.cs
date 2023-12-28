using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Mapping;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;

namespace Wolfe.SpaceTraders.Infrastructure.Data;

internal class SpaceTradersFileSystemDataClient(IOptions<SpaceTradersDataOptions> options) : ISpaceTradersDataClient
{
    private readonly SpaceTradersDataOptions _options = options.Value;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public Task AddMarketplace(Marketplace marketplace, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectory, $"{marketplace.SystemId.Value}/{marketplace.Id.Value}.json");
        return AddItem(file, marketplace, m => m.ToData(), cancellationToken);
    }

    public Task AddSystem(StarSystem system, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.SystemsDirectory, $"{system.Id.Value}.json");
        return AddItem(file, system, s => s.ToData(), cancellationToken);
    }

    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectory, $"{waypoint.SystemId.Value}/{waypoint.Id.Value}.json");
        return AddItem(file, waypoint, w => w.ToData(), cancellationToken);
    }

    public async Task<string?> GetAccessToken(CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.AccessTokensDirectory, "token.json");
        var data = await ReadItem<DataItem<DataAccessToken>>(file, cancellationToken);
        return data?.Item.Token;
    }

    public Task<DataItemResponse<Marketplace>?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectory, $"{marketplaceId.System.Value}/{marketplaceId.Value}.json");
        return GetItem<Marketplace, DataMarketplace>(file, m => m.ToDomain(), cancellationToken);
    }

    public IAsyncEnumerable<DataItemResponse<Marketplace>>? GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectory, $"{systemId.Value}");
        return GetList<Marketplace, DataMarketplace>(file, m => m.ToDomain(), w => w.SystemId == systemId, cancellationToken);
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

    public Task<DataItemResponse<Waypoint>?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectory, $"{waypointId.System.Value}/{waypointId.Value}.json");
        return GetItem<Waypoint, DataWaypoint>(file, m => m.ToDomain(), cancellationToken);
    }

    public IAsyncEnumerable<DataItemResponse<Waypoint>>? GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectory, $"{systemId.Value}");
        return GetList<Waypoint, DataWaypoint>(file, w => w.ToDomain(), w => w.SystemId == systemId, cancellationToken);
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