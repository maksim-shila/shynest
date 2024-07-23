using BuildYourHead.Api.Controllers.Requests.RecipeProduct;
using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.RequestHandlers.RecipeProduct;

public class PutRecipeProductsRequestHandler : IRequestHandler
{
    private readonly IRecipeService _recipeService;

    public PutRecipeProductsRequestHandler(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    internal string Handle(int recipeId, PutRecipeProductsRequest request)
    {
        if (recipeId <= 0)
        {
            throw new ValidationException("Recipe id should be greater than zero");
        }

        if (request.ProductsIds.Count == 0)
        {
            throw new ValidationException("Provide at least 1 product to add");
        }

        _recipeService.AddProducts(recipeId, request.ProductsIds);

        return "Products successfully added";
    }
}