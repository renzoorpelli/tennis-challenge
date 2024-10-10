using TennisChallenge.Core.Entities;

namespace TennisChallenge.Core.Repositories;

public interface IEntityRepository<TEntity>
    where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken);
    
    Task CreateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken);

    void Update(TEntity entity);
}