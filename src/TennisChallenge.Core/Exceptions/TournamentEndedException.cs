namespace TennisChallenge.Core.Exceptions;

public class TournamentNotFoundException: DomainException
{
    public TournamentNotFoundException(): base("The specified Tournament doesn't exist")
    {
        
    }
}
public class TournamentEndedException : DomainException
{
    public TournamentEndedException(): base("The Tournament ended")
    {
        
    }
}

public class TournamentWithoutPlayersException : DomainException
{
    public TournamentWithoutPlayersException(): base("The Tournament doesn't have players")
    {
        
    }
}