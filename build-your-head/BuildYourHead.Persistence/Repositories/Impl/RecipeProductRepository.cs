using BuildYourHead.Persistence.Entities;
using BuildYourHead.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Persistence.Repositories.Impl
{
    internal class RecipeProductRepository : RepositoryBase<RecipeProductEntity, int>, IRecipeProductRepository
    {
        public RecipeProductRepository(ApplicationContext context) : base(context) { }

        public void Add(int recipeId, IList<int> productsIds)
        {
            var entities = productsIds.Select(productId => new RecipeProductEntity
            {
                ProductId = productId,
                RecipeId = recipeId
            });
            DbSet.AddRange(entities);
        }

        public IList<ProductEntity> FindProductsByRecipeId(int recipeId)
        {
            return DbSet
                .Where(rp => rp.RecipeId == recipeId)
                .Include(rp => rp.Product)
                .Select(rp => rp.Product)
                .ToList();
        }
    }
}
