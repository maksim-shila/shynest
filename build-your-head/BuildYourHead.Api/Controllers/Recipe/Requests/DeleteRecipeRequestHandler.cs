using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Recipe.Requests
{
    public class DeleteRecipeRequestHandler : IRequestHandler
    {
        private readonly IRecipeService _recipeService;

        public DeleteRecipeRequestHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public string Handle(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Recipe id should be greater than zero");
            }

            _recipeService.Delete(id);
            return $"Recipe {id} successfully removed";
        }
    }
}
