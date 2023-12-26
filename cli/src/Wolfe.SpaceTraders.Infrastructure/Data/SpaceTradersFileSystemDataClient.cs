using Microsoft.Extensions.Options;
using System.Text.Json;
using Wolfe.SpaceTraders.Domain.Marketplace;
using Wolfe.SpaceTraders.Domain.Systems;
using Wolfe.SpaceTraders.Domain.Waypoints;
using Wolfe.SpaceTraders.Infrastructure.Data.Mapping;
using Wolfe.SpaceTraders.Infrastructure.Data.Models;
using Wolfe.SpaceTraders.Infrastructure.Data.Responses;

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
        var file = Path.Combine(_options.MarketplacesDirectoryPath, $"{marketplace.Symbol.Value}.json");
        return AddItem(file, marketplace, m => m.ToData(), cancellationToken);
    }

    public Task AddWaypoints(SystemSymbol systemId, IEnumerable<Waypoint> waypoints, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectoryPath, $"{systemId.Value}.json");
        return AddList(file, waypoints, w => w.ToData(), cancellationToken);
    }

    public Task<DataItemResponse<Marketplace>> GetMarketplace(WaypointSymbol marketplaceId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.MarketplacesDirectoryPath, $"{marketplaceId.Value}.json");
        return GetItem<Marketplace, DataMarketplace>(file, m => m.ToDomain(), cancellationToken);
    }

    public Task<DataListResponse<Waypoint>> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectoryPath, $"{systemId.Value}.json");
        return GetList<Waypoint, DataWaypoint>(file, w => w.ToDomain(), cancellationToken);
    }

    private Task AddItem<TDomain, TData>(string file, TDomain item, Func<TDomain, TData> map, CancellationToken cancellationToken = default)
    {
        var data = new DataItem<TData>
        {
            RetrievedAt = DateTimeOffset.UtcNow,
            Item = map(item)
        };
        return WriteJson(file, data, cancellationToken);
    }

    private Task AddList<TDomain, TData>(string file, IEnumerable<TDomain> items, Func<TDomain, TData> map, CancellationToken cancellationToken = default)
    {
        var data = new DataList<TData>
        {
            RetrievedAt = DateTimeOffset.UtcNow,
            Items = items.Select(map).ToList()
        };

        return WriteJson(file, data, cancellationToken);
    }

    private async Task<DataItemResponse<TDomain>> GetItem<TDomain, TData>(string file, Func<TData, TDomain> map, CancellationToken cancellationToken = default)
    {
        var data = await ReadJson<DataItem<TData>>(file, cancellationToken);
        if (data == null) { return DataItemResponse<TDomain>.None; }

        return new DataItemResponse<TDomain>
        {
            RetrievedAt = data.RetrievedAt,
            Item = map(data.Item)
        };
    }

    private async Task<DataListResponse<TDomain>> GetList<TDomain, TData>(string file, Func<TData, TDomain> map, CancellationToken cancellationToken = default)
    {
        var data = await ReadJson<DataList<TData>>(file, cancellationToken);
        if (data == null) { return DataListResponse<TDomain>.None; }

        return new DataListResponse<TDomain>
        {
            RetrievedAt = data.RetrievedAt,
            Items = data.Items.Select(map).ToList()
        };
    }

    private async Task WriteJson<T>(string path, T data, CancellationToken cancellationToken = default)
    {
        using var stream = new MemoryStream();
        await JsonSerializer.SerializeAsync(stream, data, _jsonOptions, cancellationToken: cancellationToken);
        stream.Position = 0;

        new FileInfo(path).Directory?.Create();
        await using var fileStream = File.Create(path);
        await stream.CopyToAsync(fileStream, cancellationToken);
    }

    private async Task<T?> ReadJson<T>(string path, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(path)) { return default; }
        await using var fileStream = File.OpenRead(path);

        var data = await JsonSerializer.DeserializeAsync<T>(fileStream, _jsonOptions, cancellationToken: cancellationToken)
                   ?? throw new Exception("Unable to deserialize data.");
        return data;
    }
}