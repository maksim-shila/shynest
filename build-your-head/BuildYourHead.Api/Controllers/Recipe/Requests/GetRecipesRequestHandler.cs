using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Recipe.Requests
{
    public class GetRecipesRequestHandler : IRequestHandler
    {
        private readonly IRecipeService _recipeService;

        public GetRecipesRequestHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public IList<RecipeDto> Handle()
        {
            return _recipeService.GetAll();
        }
    }
}
