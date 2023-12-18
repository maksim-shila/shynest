using BuildYourHead.Api.Controllers.Product.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuildYourHead.Api.Controllers.Product
{
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("/api/product/")]
        public IActionResult Get()
        {
            var handler = GetRequestHandler<GetProductsRequestHandler>();
            var result = handler.Handle();
            return Ok(result);
        }

        [HttpPut]
        [Route("/api/product/")]
        public IActionResult Put(AddProductRequest request)
        {
            var handler = GetRequestHandler<AddProductRequestHandler>();
            var result = handler.Handle(request);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/product/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var handler = GetRequestHandler<GetProductRequestHandler>();
            var result = handler.Handle(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/product/{id}")]
        public IActionResult Post([FromRoute] int id, UpdateProductRequest request)
        {
            var handler = GetRequestHandler<UpdateProductRequestHandler>();
            var result = handler.Handle(id, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/product/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var handler = GetRequestHandler<DeleteProductRequestHandler>();
            var result = handler.Handle(id);
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/product/{id}/image")]
        public IActionResult PostImage([FromRoute] int id, PostProductImageRequest request)
        {
            var handler = GetRequestHandler<PostProductImageRequestHandler>();
            var result = handler.Handle(id, request);
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/product/{id}/image/primary")]
        public IActionResult GetPrimaryImage([FromRoute] int id)
        {
            var handler = GetRequestHandler<GetPrimaryImageRequestHandler>();
            var result = handler.Handle(id);
            return Ok(result);
        }
    }
}
