using TennisChallenge.Core.Entities.Matches;
using TennisChallenge.Core.Entities.Players;

namespace TennisChallenge.Core.Entities.Tournaments;

public class Tournament
    : Entity
{
    public Tournament(Guid id, DateTime startDate, DateTime endDate, string name, string tournamentType, int matchesCount) : base(id)
    {
        StartDate = startDate.ToUniversalTime();
        EndDate = endDate.ToUniversalTime();
        TournamentType = tournamentType;
        Name = name;
        MatchesCount = matchesCount;
    }

    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid? WinnerId { get; set; }
    public string TournamentType { get; set; }
    public ICollection<Player> Players { get; private set; }
    public ICollection<Match> Matches { get; private set; }
    public int MatchesCount { get; set; }
    public static Tournament Create(Guid? id, DateTime startedDate, DateTime endedDate, string name,
        string tournamentType, int matchesCount)
        => new(id ?? Guid.NewGuid(), startedDate, endedDate, name, tournamentType, matchesCount);
}