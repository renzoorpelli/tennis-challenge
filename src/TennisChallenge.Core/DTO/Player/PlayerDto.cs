namespace TennisChallenge.Core.DTO.Player;

public record PlayerDto
{
    public Guid Id { get; set; }
    public uint Level { get; set; }
    public uint Age { get; set; }
    public string Name { get; set; } = null!;
}

public sealed record MalePlayerDto : PlayerDto
{
    public uint Force { get; set; }
    public uint Velocity { get; set; }
}

public sealed record FemalePlayerDto : PlayerDto
{
    public uint ReactionTime { get; set; }
}