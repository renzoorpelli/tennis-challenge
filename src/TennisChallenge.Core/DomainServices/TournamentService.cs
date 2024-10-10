using TennisChallenge.Core.DTO.Player;
using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;
using TennisChallenge.Core.Exceptions;

namespace TennisChallenge.Core.DomainServices;


public interface ITournamentService
{
    bool TryValidateTournament(Tournament? tournament, IReadOnlyCollection<Player> players);
}
internal sealed class TournamentService
: ITournamentService
{
    public bool TryValidateTournament(Tournament? tournament, IReadOnlyCollection<Player> players)
    {
        if (tournament is null)
            throw new TournamentNotFoundException();

        if (DateTime.UtcNow > tournament.EndDate)
            throw new TournamentEndedException();
        
        if (players.Count == 0)
            throw new TournamentWithoutPlayersException();

        return true;
    }
}