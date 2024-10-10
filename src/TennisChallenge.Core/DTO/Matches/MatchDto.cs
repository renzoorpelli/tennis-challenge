namespace TennisChallenge.Core.DTO.Matches;

public record MatchDto
{
    public Guid PlayerOneId { get; set; }
    public Guid PlayerTwoId { get; set; }
    public DateTime MatchDate { get; set; }
}