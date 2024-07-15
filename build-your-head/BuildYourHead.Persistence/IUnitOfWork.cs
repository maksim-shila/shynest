using BuildYourHead.Persistence.Repositories.Interfaces;

namespace BuildYourHead.Persistence;

public interface IUnitOfWork
{
    IProductRepository Products { get; }
    IProductImageRepository ProductImages { get; }
    IRecipeRepository Recipes { get; }
    IRecipeProductRepository RecipeProducts { get; }
    void Save();
}