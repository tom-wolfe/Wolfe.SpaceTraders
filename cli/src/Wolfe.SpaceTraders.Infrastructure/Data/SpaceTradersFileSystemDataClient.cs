using Microsoft.Extensions.Options;
using System.Text.Json;
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

    public Task AddWaypoints(SystemSymbol systemId, IEnumerable<Waypoint> waypoints, CancellationToken cancellationToken = default)
    {
        var data = new DataSystemWaypoints
        {
            RetrievedAt = DateTimeOffset.UtcNow,
            Waypoints = waypoints.Select(w => w.ToData()).ToList()
        };

        var file = Path.Combine(_options.WaypointsDirectoryPath, $"{systemId.Value}.json");
        return WriteJson(file, data, cancellationToken);
    }

    public async Task<GetWaypointsResponse> GetWaypoints(SystemSymbol systemId, CancellationToken cancellationToken = default)
    {
        var file = Path.Combine(_options.WaypointsDirectoryPath, $"{systemId.Value}.json");
        var data = await ReadJson<DataSystemWaypoints>(file, cancellationToken);
        if (data == null) { return GetWaypointsResponse.None; }

        return new GetWaypointsResponse
        {
            RetrievedAt = data.RetrievedAt,
            Waypoints = data.Waypoints.Select(w => w.ToDomain()).ToList()
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