using Microsoft.EntityFrameworkCore;
using TennisChallenge.Core.Entities;
using TennisChallenge.Core.Repositories;

namespace TennisChallenge.Infrastructure.Data.Repositories;

internal class BaseEntityRepository<TEntity> : IEntityRepository<TEntity>
    where TEntity : Entity
{
    internal readonly ApplicationDbContext DbContext;

    public BaseEntityRepository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        => await DbContext
            .Set<TEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await DbContext
            .Set<TEntity>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

    public virtual async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
        => await DbContext
            .Set<TEntity>()
            .AddAsync(entity, cancellationToken);

    public async Task CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
    {
        await DbContext
            .Set<TEntity>()
            .AddRangeAsync(entities, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }
}