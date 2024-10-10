using TennisChallenge.Core.DTO.Player;

namespace TennisChallenge.Core.Entities.Players;

public sealed class MalePlayer
    : Player
{
    public MalePlayer(
        Guid id,
        string name,
        string country,
        uint level,
        uint age,
        string gender,
        uint force,
        uint velocity,
        uint wins,
        uint losses) : base(id, name, country, level, age, gender, wins, losses)
    {
        Force = force;
        Velocity = velocity;
    }

    public uint Force { get; private set; }
    public uint Velocity { get; private set; }

    public override int CalculateWinProbability()
    {
        var statScore = (Force + Velocity) / 2; // avg 
        var historyScore = Wins - Losses;
        var randomFactor = new Random().Next(0, 10);
        
        var winProbabilitty = checked((int)(0.7 * statScore + 0.2 * historyScore + 0.1 * randomFactor));

        return Math.Clamp(winProbabilitty, 0, 100);
    }

    public static MalePlayer Create(
        Guid? id,
        string name,
        string country,
        uint level,
        uint age,
        string gender,
        uint force,
        uint velocity,
        uint wins,
        uint losses)
        => new(id ?? Guid.NewGuid(), name, country, level, age, gender, force, velocity, wins, losses);

    public override MalePlayerDto ToDto()
        => new MalePlayerDto
        {
            Id = this.Id,
            Age = this.Age,
            Name = this.Name,
            Level = this.Level,
            Velocity = this.Velocity,
            Force = this.Force
        };
}