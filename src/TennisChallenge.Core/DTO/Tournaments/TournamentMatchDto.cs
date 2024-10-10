using TennisChallenge.Core.DTO.Player;

namespace TennisChallenge.Core.DTO.Tournaments;

public class TournamentMatchDto
{
    public Guid MatchId { get; set; } 
    public int Round { get; set; }
    public DateTime MatchDate { get; set; }
    public PlayerDto PlayerOne { get; set; }= null!;
    public PlayerDto PlayerTwo { get; set; }= null!;
    public string? WinnerName { get; set; }
   
}