using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Recipe.Requests
{
    public class UpdateRecipeRequestHandler : IRequestHandler
    {
        private IRecipeService _recipeService;

        public UpdateRecipeRequestHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        internal RecipeDto Handle(int id, UpdateRecipeRequest request)
        {
            if (id <= 0)
            {
                throw new ValidationException("Recipe id should be greater than zero");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("Recipe name should be present");
            }
            var dto = new RecipeDto
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
            };

            return _recipeService.Update(dto);
        }
    }
}
