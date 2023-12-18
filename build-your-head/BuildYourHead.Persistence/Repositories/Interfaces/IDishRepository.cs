using BuildYourHead.Persistence.Entities;
using System.Linq.Expressions;

namespace BuildYourHead.Persistence.Repositories.Interfaces
{
    public interface IRecipeRepository : IRepository<RecipeEntity, int>
    {
    }
}
