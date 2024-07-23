using BuildYourHead.Api.Controllers.RequestHandlers.Product;
using BuildYourHead.Api.Controllers.Requests.Product;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var handler = GetRequestHandler<GetProductsRequestHandler>();
        var result = handler.Handle();
        return Ok(result);
    }

    [HttpPut]
    public IActionResult Put(AddProductRequest request)
    {
        var handler = GetRequestHandler<AddProductRequestHandler>();
        var result = handler.Handle(request);
        return Ok(result);
    }

    [HttpGet("{productId}")]
    public IActionResult Get([FromRoute] int productId)
    {
        var handler = GetRequestHandler<GetProductRequestHandler>();
        var result = handler.Handle(productId);
        return Ok(result);
    }

    [HttpPost("{productId}")]
    public IActionResult Post([FromRoute] int productId, UpdateProductRequest request)
    {
        var handler = GetRequestHandler<UpdateProductRequestHandler>();
        var result = handler.Handle(productId, request);
        return Ok(result);
    }

    [HttpDelete("{productId}")]
    public IActionResult Delete([FromRoute] int productId)
    {
        var handler = GetRequestHandler<DeleteProductRequestHandler>();
        var result = handler.Handle(productId);
        return Ok(result);
    }

    [HttpPost("{productId}/image")]
    public IActionResult PostImage([FromRoute] int productId, PostProductImageRequest request)
    {
        var handler = GetRequestHandler<PostProductImageRequestHandler>();
        var result = handler.Handle(productId, request);
        return Ok(result);
    }

    [HttpGet("{productId}/image/primary")]
    public IActionResult GetPrimaryImage([FromRoute] int productId)
    {
        var handler = GetRequestHandler<GetProductPrimaryImageRequestHandler>();
        var result = handler.Handle(productId);
        return Ok(result);
    }
}