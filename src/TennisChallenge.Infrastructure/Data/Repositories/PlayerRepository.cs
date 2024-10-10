using Microsoft.EntityFrameworkCore;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Infrastructure.Data.Repositories;

internal sealed class PlayerRepository
    : BaseEntityRepository<Player>, IPlayerRepository

{
    public PlayerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<Player>> GetPlayersList(List<Guid> playerIds, CancellationToken cancellationToken)
        => await DbContext.Players
            .AsNoTracking()
            .Where(p => playerIds.Contains(p.Id))
            .ToListAsync(cancellationToken);
}