namespace TennisChallenge.Infrastructure.Data;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken);
}