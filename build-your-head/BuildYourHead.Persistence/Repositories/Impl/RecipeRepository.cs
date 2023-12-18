using BuildYourHead.Persistence.Entities;
using BuildYourHead.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BuildYourHead.Persistence.Repositories.Impl
{
    internal class RecipeRepository : RepositoryBase<RecipeEntity, int>, IRecipeRepository
    {
        public RecipeRepository(ApplicationContext context) : base(context) { }
    }
}
