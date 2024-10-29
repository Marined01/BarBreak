namespace BarBreak.Infrastructure.Repositories;

using BarBreak.Core.Repositories;

public class RepositoryBase<TKey, TEntity, TContext>(TContext dbContext)
    : IRepository<TKey, TEntity>
    where TKey : notnull
    where TEntity : BaseEntity<TKey>
    where TContext : DbContext
{
    public async Task<TEntity?> GetById(TKey id)
    {
        return await dbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id!.Equals(id));
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await dbContext.Set<TEntity>()
            .AsNoTracking().ToArrayAsync();
    }

    public async Task<TEntity> Create(TEntity entity)
    {
        await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task Update(TEntity entity)
    {
        dbContext.Set<TEntity>().Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(TKey id)
    {
        var entity = await this.GetById(id);
        if (entity is null)
        {
            return;
        }

        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}