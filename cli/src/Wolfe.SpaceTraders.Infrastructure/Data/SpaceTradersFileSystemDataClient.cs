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
        var file = Path.Combine(_options.MarketplacesDirectoryPath, $"{marketplace.SystemId.Value}/{marketplace.Id.Value}.json");
        return AddItem(file, marketplace, m => m.ToData(), cancellationToken);
    }

    public Task AddWaypoint(Waypoint waypoint, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectoryPath, $"{waypoint.SystemId.Value}/{waypoint.Id.Value}.json");
        return AddItem(file, waypoint, m => m.ToData(), cancellationToken);
    }

    public Task<DataItemResponse<Marketplace>?> GetMarketplace(WaypointId marketplaceId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectoryPath, $"{marketplaceId.System.Value}/{marketplaceId.Value}.json");
        return GetItem<Marketplace, DataMarketplace>(file, m => m.ToDomain(), cancellationToken);
    }

    public IAsyncEnumerable<DataItemResponse<Marketplace>>? GetMarketplaces(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectoryPath, $"{systemId.Value}");
        return GetList<Marketplace, DataMarketplace>(file, m => m.ToDomain(), w => w.SystemId == systemId, cancellationToken);
    }

    public Task<DataItemResponse<Waypoint>?> GetWaypoint(WaypointId waypointId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectoryPath, $"{waypointId.System.Value}/{waypointId.Value}.json");
        return GetItem<Waypoint, DataWaypoint>(file, m => m.ToDomain(), cancellationToken);
    }

    public IAsyncEnumerable<DataItemResponse<Waypoint>>? GetWaypoints(SystemId systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectoryPath, $"{systemId.Value}");
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