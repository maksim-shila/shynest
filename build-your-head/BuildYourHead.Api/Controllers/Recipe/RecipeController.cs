using BuildYourHead.Api.Controllers.Recipe.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildYourHead.Api.Controllers.Recipe
{
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
        [Route("/api/recipe/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var handler = GetRequestHandler<GetRecipeRequestHandler>();
            var result = handler.Handle(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/recipe/{id}")]
        public IActionResult Post([FromRoute] int id, UpdateRecipeRequest request)
        {
            var handler = GetRequestHandler<UpdateRecipeRequestHandler>();
            var result = handler.Handle(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/recipe/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var handler = GetRequestHandler<DeleteRecipeRequestHandler>();
            var result = handler.Handle(id);
            return Ok(result);
        }
    }
}
