using BuildYourHead.Application.Dto;
using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Application.Mappers.Interfaces
{
    public interface IRecipeMapper : IMapper<RecipeDto, RecipeEntity>
    {
    }
}
