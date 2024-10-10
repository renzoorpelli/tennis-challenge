using TennisChallenge.Core.Entities.Players;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Core.Entities.Matches;

public sealed class Match
    : Entity
{
    public Match(Guid id, Guid playerOneId, Guid playerTwoId, Guid tournamentId, int playerOnePoints,
        int playerTwoPoints, Guid? winnerId, DateTime matchDate, int round)
        : base(id)
    {
        PlayerOneId = playerOneId;
        PlayerTwoId = playerTwoId;
        TournamentId = tournamentId;
        PlayerOnePoints = playerOnePoints;
        PlayerTwoPoints = playerTwoPoints;
        WinnerId = winnerId;
        MatchDate = matchDate;
        Round = round;
    }
    public Guid PlayerOneId { get; set; }
    public Guid PlayerTwoId { get; set; }
    public Guid TournamentId { get; set; }
    public int Round { get; set; }
    public int PlayerOnePoints { get; set; }
    public int PlayerTwoPoints { get; set; }
    public Guid? WinnerId { get; set; }
    public DateTime MatchDate { get; set; }


    public Player PlayerOne { get; set; } = null!;
    public Player PlayerTwo { get; set; }= null!;

  
    public Tournament Tournament { get; set; }= null!;

    public static Match Create(
        Guid? id, Guid playerOneId, Guid playerTwoId, Guid tournamentId, int playerOnePoints,
        int playerTwoPoints, Guid? winnerId, DateTime matchDate, int round)
        => new(id ?? Guid.NewGuid(), playerOneId, playerTwoId, tournamentId, playerOnePoints,
            playerTwoPoints, winnerId, matchDate, round);
    
    public static Match CreateInitial(
        Guid? id, Guid playerOneId, Guid playerTwoId, Guid tournamentId,
        DateTime matchDate, int round)
        => new(id ?? Guid.NewGuid(), playerOneId, playerTwoId, tournamentId, 0,
            0, null, matchDate, round);
}