using TennisChallenge.Core.Entities.Players;

namespace TennisChallenge.Core.Repositories;

public interface IPlayerRepository : IEntityRepository<Player>
{
    public Task<List<Player>> GetPlayersList(List<Guid> playerIds, CancellationToken cancellationToken);
}