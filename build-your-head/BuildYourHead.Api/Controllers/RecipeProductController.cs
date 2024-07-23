using BuildYourHead.Api.Controllers.RequestHandlers.RecipeProduct;
using BuildYourHead.Api.Controllers.Requests.RecipeProduct;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

[ApiController]
[Route("/api/recipe/{recipeId}/product")]
public class RecipeProductController : ControllerBase
{
    [HttpGet]
    public IActionResult Get([FromRoute] int recipeId)
    {
        var handler = GetRequestHandler<GetRecipeProductsRequestHandler>();
        var result = handler.Handle(recipeId);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Put([FromRoute] int recipeId, PutRecipeProductsRequest request)
    {
        var handler = GetRequestHandler<PutRecipeProductsRequestHandler>();
        var result = handler.Handle(recipeId, request);
        return Ok(result);
    }

    [HttpDelete("{productId}")]
    public IActionResult Delete([FromRoute] int recipeId, [FromRoute] int productId)
    {
        var handler = GetRequestHandler<DeleteRecipeProductsRequestHandler>();
        var result = handler.Handle(recipeId, productId);
        return Ok(result);
    }
}