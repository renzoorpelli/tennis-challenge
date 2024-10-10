using TennisChallenge.Core.Entities.Matches;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Infrastructure.Data.Repositories;

internal sealed class MatchRepository 
    : BaseEntityRepository<Match>, IMatchRepository
{
    public MatchRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}