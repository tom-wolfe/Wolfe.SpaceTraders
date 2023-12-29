namespace Wolfe.SpaceTraders.Domain.Missions;

public interface IMission
{
    public Task Execute(CancellationToken cancellationToken = default);
}
