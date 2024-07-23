using BuildYourHead.Persistence.Entities;
using BuildYourHead.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Persistence.Repositories.Impl;

internal class RecipeRepository : RepositoryBase<RecipeEntity, int>, IRecipeRepository
{
    public RecipeRepository(DbContext context) : base(context)
    {
    }
}