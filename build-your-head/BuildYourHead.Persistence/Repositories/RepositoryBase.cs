using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Persistence.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
{
    protected RepositoryBase(DbContext context)
    {
        DbSet = context.Set<TEntity>();
    }

    protected DbSet<TEntity> DbSet { get; }

    public virtual TEntity? Get(TKey id)
    {
        return DbSet.Find(id);
    }

    public virtual TEntity? Get(params object[] keys)
    {
        return DbSet.Find(keys);
    }

    public virtual IEnumerable<TEntity> Get()
    {
        return DbSet.ToList();
    }

    public virtual TEntity Create(TEntity entity)
    {
        var entry = DbSet.Add(entity);
        return entry.Entity;
    }

    public virtual TEntity Update(TEntity entity)
    {
        var entry = DbSet.Update(entity);
        return entry.Entity;
    }

    public virtual void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }
}