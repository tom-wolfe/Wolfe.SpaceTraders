using System.Reactive.Linq;
using System.Reactive.Subjects;
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
    protected readonly Subject<FormattableString> _log = new();
    protected readonly Subject<Exception> _error = new();

    /// <summary>
    /// Creates a new instance of <see cref="Mission"/>.
    /// </summary>
    /// <param name="startingStatus">The status that the mission will start in.</param>
    /// <param name="ship">The ship executing the mission.</param>
    /// <param name="scheduler">The object that will be used to handle the running of the mission.</param>
    protected Mission(MissionStatus startingStatus, Ship ship, IMissionScheduler scheduler)
    {
        _scheduler = scheduler;
        Ship = ship;
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
    /// Gets the ship executing the mission.
    /// </summary>
    protected Ship Ship { get; }

    /// <inheritdoc/>
    public IObservable<MissionStatus> StatusChanged => _status.AsObservable();

    /// <inheritdoc/>
    public IObservable<FormattableString> Event => _log.AsObservable();

    /// <inheritdoc/>
    public IObservable<Exception> Error => _error.AsObservable();

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
            _error.OnNext(ex);
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
