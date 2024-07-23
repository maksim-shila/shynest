namespace BuildYourHead.Persistence.Repositories;

public interface IRepository<TEntity, TKey> where TEntity : class
{
    TEntity? Get(TKey id);
    TEntity? Get(params object[] ids);
    IEnumerable<TEntity> Get();
    TEntity Create(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}