using BuildYourHead.Application.Dto;

namespace BuildYourHead.Application.Services
{
    public interface IRecipeService
    {
        IList<RecipeDto> GetAll();
        RecipeDto Get(int id);
        RecipeDto Add(RecipeDto recipe);
        RecipeDto Update(RecipeDto recipe);
        void Delete(int id);
        IList<ProductDto> GetProducts(int recipeId);
        void AddProducts(int recipeId, IList<int> productsIds);
        void DeleteRecipeProduct(int recipeId, int productId);
    }
}
