using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Recipe.Requests
{
    public class GetRecipeRequestHandler : IRequestHandler
    {
        private IRecipeService _recipeService;

        public GetRecipeRequestHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public RecipeDto Handle(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Recipe id should be greater than zero");
            }

            return _recipeService.Get(id);
        }
    }
}
