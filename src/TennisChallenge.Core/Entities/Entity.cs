namespace TennisChallenge.Core.Entities;

public class Entity
{
    internal Entity(Guid id)
    {
        Id = id;
    }
    public Guid Id { get; private set; }
}