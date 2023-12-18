using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.RecipeProducts.Requests
{
    public class DeleteRecipeProductsRequestHandler : IRequestHandler
    {
        private IRecipeService _recipeService;

        public DeleteRecipeProductsRequestHandler(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        public string Handle(int recipeId, int productId)
        {
            if (recipeId <= 0)
            {
                throw new ValidationException("Recipe id should be greater than zero");
            }
            if (productId <= 0)
            {
                throw new ValidationException("Product id should be greater than zero");
            }

            _recipeService.DeleteRecipeProduct(recipeId, productId);
            return "Product successfully removed";
        }
    }
}
