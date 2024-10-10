using Microsoft.EntityFrameworkCore;
using TennisChallenge.Core.DTO.Tournaments;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Infrastructure.Data.Repositories;

internal sealed class TournamentRepository
    : BaseEntityRepository<Tournament>, ITournamentRepository
{
    public TournamentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public Task<bool> ExistAsync(string name, string type, CancellationToken cancellationToken)
    {
        return DbContext.Tournaments.AsNoTracking().AnyAsync(
            x => EF.Functions.ILike(x.Name, name) &&
                 EF.Functions.ILike(x.TournamentType, type), cancellationToken);
    }

    public async Task AddTournamentPlayersAsync(List<PlayerTournament> players, CancellationToken cancellationToken)
    {
        await this.DbContext.PlayerTournaments.AddRangeAsync(players, cancellationToken);
    }

    public async Task<List<Player>> GetTournamentPlayersAsync(Guid tournamentId,
        CancellationToken cancellationToken)
    {
        var players = await DbContext.PlayerTournaments
            .AsNoTracking()
            .Where(pt => pt.TournamentId == tournamentId)
            .Include(pt => pt.Player)
            .Select(pt => pt.Player)
            .ToListAsync(cancellationToken);

        return players;
    }

    public async Task<bool> CanPlayAsync(HashSet<Guid> playerIds, string tournamentType,
        CancellationToken cancellationToken)
    {
        // Query to check if any of the players are in another active tournament
        var isAnyPlayerInAnotherTournament = await DbContext.Tournaments
            .AsNoTracking()
            .Include(t => t.Players)
            .Where(t => t.EndDate > DateTime.UtcNow) // Tournaments that are still ongoing
            .AnyAsync(t => t.Players.Any(p => playerIds.Contains(p.Id) && p.Gender == tournamentType),
                cancellationToken);

        // If any player is in another ongoing tournament, return false
        return !isAnyPlayerInAnotherTournament;
    }

    public async Task<bool> ContainMatchesAsync(Guid tournamentId, CancellationToken cancellationToken)
        => await DbContext.Matches.AsNoTracking().AnyAsync(x => x.TournamentId == tournamentId, cancellationToken);

    public async Task<List<TournamentMatchDto>> GetTournamentMatchesAsync(Guid tournamentId,
        CancellationToken cancellationToken)
        =>
            await DbContext.Matches
                .AsNoTracking()
                .Where(m => m.TournamentId == tournamentId)
                .Include(p => p.PlayerOne)
                .Include(p => p.PlayerTwo)
                .Select(agg => new TournamentMatchDto()
                {
                    MatchId = agg.Id,
                    MatchDate = agg.MatchDate,
                    PlayerOne = agg.PlayerOne.ToDto(),
                    PlayerTwo = agg.PlayerTwo.ToDto(),
                    Round = agg.Round,
                    WinnerName = agg.WinnerId == agg.PlayerOneId
                        ? agg.PlayerOne.Name
                        : agg.WinnerId == agg.PlayerTwoId
                            ? agg.PlayerTwo.Name
                            : null
                })
                .OrderBy(m => m.Round)
                .ToListAsync(cancellationToken);
}