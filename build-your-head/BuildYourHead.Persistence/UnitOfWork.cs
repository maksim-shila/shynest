using BuildYourHead.Persistence.Repositories.Impl;
using BuildYourHead.Persistence.Repositories.Interfaces;

namespace BuildYourHead.Persistence;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ApplicationContext context)
    {
        Products = new ProductRepository(context);
        ProductImages = new ProductImageRepository(context);
        Recipes = new RecipeRepository(context);
        RecipeProducts = new RecipeProductRepository(context);
        Images = new ImageRepository(context);
        Context = context;
    }

    private ApplicationContext Context { get; }

    #region Repositories

    public IProductRepository Products { get; }
    public IProductImageRepository ProductImages { get; }
    public IRecipeRepository Recipes { get; }
    public IRecipeProductRepository RecipeProducts { get; }
    public IImageRepository Images { get; }

    #endregion Repositories

    public void Save()
    {
        Context.SaveChanges();
    }
}