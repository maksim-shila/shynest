using BuildYourHead.Api.Controllers.RequestHandlers.RecipeProduct;
using BuildYourHead.Api.Controllers.Requests.RecipeProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

[Authorize]
[ApiController]
public class RecipeProductController : ControllerBase
{
    [HttpGet]
    [Route("/api/recipe/{recipeId:int}/product")]
    public IActionResult Get([FromRoute] int recipeId)
    {
        var handler = GetRequestHandler<GetRecipeProductsRequestHandler>();
        var result = handler.Handle(recipeId);
        return Ok(result);
    }

    [HttpPut]
    [Route("/api/recipe/{recipeId:int}/product")]
    public IActionResult Put([FromRoute] int recipeId, PutRecipeProductsRequest request)
    {
        var handler = GetRequestHandler<PutRecipeProductsRequestHandler>();
        var result = handler.Handle(recipeId, request);
        return Ok(result);
    }

    [HttpDelete]
    [Route("/api/recipe/{recipeId:int}/product/{productId:int}")]
    public IActionResult Delete([FromRoute] int recipeId, [FromRoute] int productId)
    {
        var handler = GetRequestHandler<DeleteRecipeProductsRequestHandler>();
        var result = handler.Handle(recipeId, productId);
        return Ok(result);
    }
}