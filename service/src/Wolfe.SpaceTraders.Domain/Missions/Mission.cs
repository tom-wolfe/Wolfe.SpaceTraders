using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json.Serialization;
using Wolfe.SpaceTraders.Domain.Agents;
using Wolfe.SpaceTraders.Domain.Missions.Scheduling;
using Wolfe.SpaceTraders.Domain.Ships;

namespace Wolfe.SpaceTraders.Domain.Missions;

/// <summary>
/// Provides some basic functionality for a ship mission.
/// </summary>
public abstract class Mission : IMission
{
    private readonly IMissionScheduler _scheduler;
    private MissionStatus _statusValue;
    private readonly Subject<MissionStatus> _status = new();

    /// <summary>
    /// Creates a new instance of <see cref="Mission"/>.
    /// </summary>
    /// <param name="startingStatus">The status that the mission will start in.</param>
    /// <param name="ship">The ship executing the mission.</param>
    /// <param name="log">An object that can be used to track the progress of the mission.</param>
    /// <param name="scheduler">The object that will be used to handle the running of the mission.</param>
    protected Mission(MissionStatus startingStatus, Ship ship, IMissionLog log, IMissionScheduler scheduler)
    {
        _scheduler = scheduler;
        Ship = ship;
        Log = log;
        Status = startingStatus;
    }

    /// <inheritdoc/>
    public required MissionId Id { get; init; }

    /// <inheritdoc/>
    public abstract MissionType Type { get; }

    /// <inheritdoc/>
    public MissionStatus Status
    {
        get
        {
            return _statusValue;
        }
        private set
        {
            _statusValue = value;
            _status.OnNext(value);
        }
    }

    /// <inheritdoc/>
    public required AgentId AgentId { get; init; }

    /// <inheritdoc/>
    public ShipId ShipId => Ship.Id;

    /// <summary>
    /// Gets an observable that will emit the current status of the mission whenever it changes.
    /// </summary>
    [JsonIgnore]
    public IObservable<MissionStatus> StatusChanged => _status.AsObservable();

    /// <summary>
    /// Gets the ship executing the mission.
    /// </summary>
    protected Ship Ship { get; }

    /// <summary>
    /// Gets the mission log.
    /// </summary>
    protected IMissionLog Log { get; }

    /// <inheritdoc/>
    public ValueTask Start(CancellationToken cancellationToken = default)
    {
        if (Status == MissionStatus.Running)
        {
            throw new InvalidOperationException("Mission is already running.");
        }
        if (Status == MissionStatus.Complete)
        {
            throw new InvalidOperationException("Mission is already complete.");
        }
        return _scheduler.Start(this, cancellationToken);
    }

    /// <inheritdoc/>
    public async ValueTask Stop(CancellationToken cancellationToken = default)
    {
        if (Status == MissionStatus.Running)
        {
            Status = MissionStatus.Stopping;
            try
            {
                await _scheduler.Stop(this, cancellationToken);
            }
            finally
            {
                Status = MissionStatus.Suspended;
            }
        }
    }

    /// <inheritdoc/>
    public async Task Execute(CancellationToken cancellationToken = default)
    {
        Status = MissionStatus.Running;
        try
        {
            await ExecuteCore(cancellationToken);
            Status = MissionStatus.Complete;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            Status = MissionStatus.Suspended;
        }
        catch (Exception ex)
        {
            Status = MissionStatus.Error;
            await Log.WriteError(ex, CancellationToken.None);
            throw;
        }
    }

    /// <summary>
    /// Executes the mission. The returned task should not complete until the mission is complete.
    /// </summary>
    /// <param name="cancellationToken">A token that can be used to terminate the mission early.</param>
    /// <returns>A task that can be used to monitor the mission.</returns>
    protected abstract Task ExecuteCore(CancellationToken cancellationToken = default);
}
