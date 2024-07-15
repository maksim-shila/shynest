using BuildYourHead.Api.Controllers.RequestHandlers.Recipe;
using BuildYourHead.Api.Controllers.Requests.Recipe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

[Authorize]
[ApiController]
public class RecipeController : ControllerBase
{
    [HttpGet]
    [Route("/api/recipe/")]
    public IActionResult Get()
    {
        var handler = GetRequestHandler<GetRecipesRequestHandler>();
        var result = handler.Handle();
        return Ok(result);
    }

    [HttpPut]
    [Route("/api/recipe/")]
    public IActionResult Put(AddRecipeRequest request)
    {
        var handler = GetRequestHandler<PutRecipeRequestHandler>();
        var result = handler.Handle(request);
        return Ok(result);
    }


    [HttpGet]
    [Route("/api/recipe/{recipeId:int}")]
    public IActionResult Get([FromRoute] int recipeId)
    {
        var handler = GetRequestHandler<GetRecipeRequestHandler>();
        var result = handler.Handle(recipeId);
        return Ok(result);
    }

    [HttpPost]
    [Route("/api/recipe/{recipeId:int}")]
    public IActionResult Post([FromRoute] int recipeId, UpdateRecipeRequest request)
    {
        var handler = GetRequestHandler<UpdateRecipeRequestHandler>();
        var result = handler.Handle(recipeId, request);
        return Ok(result);
    }

    [HttpDelete]
    [Route("/api/recipe/{recipeId:int}")]
    public IActionResult Delete([FromRoute] int recipeId)
    {
        var handler = GetRequestHandler<DeleteRecipeRequestHandler>();
        var result = handler.Handle(recipeId);
        return Ok(result);
    }
}