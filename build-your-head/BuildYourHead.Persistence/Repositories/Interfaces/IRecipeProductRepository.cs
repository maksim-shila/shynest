using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Persistence.Repositories.Interfaces
{
    public interface IRecipeProductRepository : IRepository<RecipeProductEntity, int>
    {
        void Add(int recipeId, IList<int> productsIds);
        IList<ProductEntity> FindProductsByRecipeId(int recipeId);
    }
}