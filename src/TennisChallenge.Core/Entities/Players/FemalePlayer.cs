using System.ComponentModel.Design;
using TennisChallenge.Core.DTO.Player;

namespace TennisChallenge.Core.Entities.Players;

public sealed class FemalePlayer
    : Player
{
    public FemalePlayer(
        Guid id,
        string name,
        string country,
        uint level,
        uint age,
        string gender,
        uint reactionTime,
        uint wins,
        uint losses) : base(id, name, country, level, age, gender, wins, losses)
    {
        ReactionTime = reactionTime;
    }

    public uint ReactionTime { get; private set; }

    public override int CalculateWinProbability()
    {
        var statScore = ReactionTime;
        var historyScore = Wins - Losses;
        var randomFactor = new Random().Next(0, 10);
        
        var winProbabilitty = checked((int)(0.7 * statScore + 0.2 * historyScore + 0.1 * randomFactor));

        return Math.Clamp(winProbabilitty, 0, 100);
    }

    public static FemalePlayer Create(
        Guid? id,
        string name,
        string country,
        uint level,
        uint age,
        string gender,
        uint reactionTime,
        uint wins,
        uint losses)
        => new(id ?? Guid.NewGuid(), name, country, level, age, gender, reactionTime, wins, losses);

    public override FemalePlayerDto ToDto()
        => new FemalePlayerDto
        {
            Id = this.Id,
            Age = this.Age,
            Level = this.Level,
            ReactionTime = this.ReactionTime
        };
}