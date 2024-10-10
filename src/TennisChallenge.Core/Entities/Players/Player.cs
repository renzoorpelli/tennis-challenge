using TennisChallenge.Core.DTO.Player;
using TennisChallenge.Core.Entities.Tournaments;

namespace TennisChallenge.Core.Entities.Players;

public class Player
    : Entity
{
    internal Player(
        Guid id,
        string name,
        string country,
        uint level,
        uint age,
        string gender,
        uint wins,
        uint losses) : base(id)
    {
        Name = name;
        Country = country;
        Level = level;
        Age = age;
        Gender = gender;
        Wins = wins;
        Losses = losses;
    }

    public string Name { get; set; }
    public string Country { get; private set; }
    public uint Level { get; set; }
    public uint Age { get; set; }
    public string Gender { get; private set; }
    public uint Wins { get; set; }
    public uint Losses { get; set; }
    public ICollection<Tournament> Tournaments { get; set; } = null!;
    
    public virtual int CalculateWinProbability()
    {
        throw new NotImplementedException();
    }
    
    public virtual PlayerDto ToDto()
        => new PlayerDto
        {
            Id = this.Id,
            Name = this.Name,
            Age = this.Age,
            Level = this.Level,
        };
    
}