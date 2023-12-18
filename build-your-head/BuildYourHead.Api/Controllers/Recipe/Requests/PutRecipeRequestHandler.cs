using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Recipe.Requests
{
    public class PutRecipeRequestHandler : IRequestHandler
    {
        private readonly IRecipeService _recipeService;

        public PutRecipeRequestHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public RecipeDto Handle(AddRecipeRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("Recipe name should be present");
            }

            var dto = new RecipeDto
            {
                Name = request.Name,
                Description = request.Description
            };
            return _recipeService.Add(dto);
        }
    }
}
