using TennisChallenge.Core.Entities.Players;

namespace TennisChallenge.Core.Entities.Tournaments;

public class PlayerTournament
{
    public PlayerTournament(Guid playerId, Guid tournamentId)
    {
        PlayerId = playerId;
        TournamentId = tournamentId;
    }
    public Guid PlayerId { get; set; }
    public Guid TournamentId { get; set; }
    
    public Player Player { get; set; }
    public Tournament Tournament { get; set; }

    public static PlayerTournament Create(Guid playerId, Guid tournamentId)
        => new PlayerTournament(playerId, tournamentId);
}