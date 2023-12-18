using System.Collections.Immutable;

namespace BuildYourHead.Application.Mappers
{
    public interface IMapper<TModel, TEntity>
        where TModel : class
        where TEntity : class
    {
        TEntity ToEntity(TModel dto);
        TModel ToDto(TEntity entity);

        public IList<TModel> ToDtos(IEnumerable<TEntity> entities)
        {
            return entities.Select(ToDto).ToImmutableList();
        }

        public IList<TEntity> ToEntities(IEnumerable<TModel> dtos)
        {
            return dtos.Select(ToEntity).ToImmutableList();
        }
    }
}
