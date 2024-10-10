using TennisChallenge.Core.DTO.Tournaments;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Core.Repositories;

public interface ITournamentRepository : IEntityRepository<Tournament>
{
    Task<bool> ExistAsync(string name, string type, CancellationToken cancellationToken);
    Task AddTournamentPlayersAsync(List<PlayerTournament> players, CancellationToken cancellationToken);
    Task<List<Player>> GetTournamentPlayersAsync(Guid tournamentId, CancellationToken cancellationToken);
    Task<bool> CanPlayAsync(HashSet<Guid> playerIds, string tournamentType, CancellationToken cancellationToken);
    Task<bool> ContainMatchesAsync(Guid tournamentId, CancellationToken cancellationToken);
    Task<List<TournamentMatchDto>> GetTournamentMatchesAsync(Guid tournamentId, CancellationToken cancellationToken);
}