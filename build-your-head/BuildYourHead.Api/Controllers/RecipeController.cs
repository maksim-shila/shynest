using BuildYourHead.Api.Controllers.RequestHandlers.Recipe;
using BuildYourHead.Api.Controllers.Requests.Recipe;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

[ApiController]
[Route("api/recipe")]
public class RecipeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var handler = GetRequestHandler<GetRecipesRequestHandler>();
        var result = handler.Handle();
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Put(AddRecipeRequest request)
    {
        var handler = GetRequestHandler<PutRecipeRequestHandler>();
        var result = handler.Handle(request);
        return Ok(result);
    }


    [HttpGet("{recipeId}")]
    public IActionResult Get([FromRoute] int recipeId)
    {
        var handler = GetRequestHandler<GetRecipeRequestHandler>();
        var result = handler.Handle(recipeId);
        return Ok(result);
    }

    [HttpPost("{recipeId}")]
    public IActionResult Post([FromRoute] int recipeId, UpdateRecipeRequest request)
    {
        var handler = GetRequestHandler<UpdateRecipeRequestHandler>();
        var result = handler.Handle(recipeId, request);
        return Ok(result);
    }

    [HttpDelete("{recipeId}")]
    public IActionResult Delete([FromRoute] int recipeId)
    {
        var handler = GetRequestHandler<DeleteRecipeRequestHandler>();
        var result = handler.Handle(recipeId);
        return Ok(result);
    }
}