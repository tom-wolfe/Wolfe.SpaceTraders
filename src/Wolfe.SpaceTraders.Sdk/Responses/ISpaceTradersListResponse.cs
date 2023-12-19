using Wolfe.SpaceTraders.Sdk.Models;

namespace Wolfe.SpaceTraders.Sdk.Responses;

public interface ISpaceTradersListResponse<out T>
{
    IEnumerable<T> Data { get; }
    SpaceTradersListMeta Meta { get; }
}