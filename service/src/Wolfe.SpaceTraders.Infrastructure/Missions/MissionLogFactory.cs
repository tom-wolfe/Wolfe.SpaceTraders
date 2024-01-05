using Wolfe.SpaceTraders.Domain.Missions;

namespace Wolfe.SpaceTraders.Infrastructure.Missions;

internal class MissionLogFactory(IEnumerable<IMissionLogProvider> providers) : IMissionLogFactory
{
    public IMissionLog CreateLog(MissionId missionId)
    {
        var loggers = providers.Select(p => p.CreateLog(missionId));
        return new AggregateMissionLog(loggers);
    }

    private class AggregateMissionLog(IEnumerable<IMissionLog> loggers) : IMissionLog
    {
        public async ValueTask Write(FormattableString message, CancellationToken cancellationToken = default)
        {
            foreach (var log in loggers)
            {
                await log.Write(message, cancellationToken);
            }
        }

        public async ValueTask WriteError(Exception ex, CancellationToken cancellationToken = default)
        {
            foreach (var log in loggers)
            {
                await log.WriteError(ex, cancellationToken);
            }
        }
    }
}
