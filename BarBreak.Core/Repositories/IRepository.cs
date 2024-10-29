namespace BarBreak.Core.Repositories;

using BarBreak.Core.Entities;

public interface IRepository<in TKey, TEntity>
    where TKey : notnull
    where TEntity : BaseEntity<TKey>
{
    Task<TEntity?> GetById(TKey id);

    Task<IEnumerable<TEntity>> GetAll();

    Task<TEntity> Create(TEntity entity);

    Task Update(TEntity entity);

    Task Delete(TKey id);
}