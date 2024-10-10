namespace TennisChallenge.Infrastructure.Data;

internal sealed class PostgresUnitOfWork 
    : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public PostgresUnitOfWork(ApplicationDbContext dbContext)
        => _dbContext = dbContext;
    
    public async Task ExecuteAsync(Func<Task> func, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await func();
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}