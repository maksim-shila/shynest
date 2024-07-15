using BuildYourHead.Api.Controllers.RequestHandlers.Product;
using BuildYourHead.Api.Controllers.Requests.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ControllerBase = BuildYourHead.Api.Controllers.Core.ControllerBase;

namespace BuildYourHead.Api.Controllers;

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
    [Route("/api/product/{productId:int}")]
    public IActionResult Get([FromRoute] int productId)
    {
        var handler = GetRequestHandler<GetProductRequestHandler>();
        var result = handler.Handle(productId);
        return Ok(result);
    }

    [HttpPost]
    [Route("/api/product/{productId:int}")]
    public IActionResult Post([FromRoute] int productId, UpdateProductRequest request)
    {
        var handler = GetRequestHandler<UpdateProductRequestHandler>();
        var result = handler.Handle(productId, request);
        return Ok(result);
    }

    [HttpDelete]
    [Route("/api/product/{productId:int}")]
    public IActionResult Delete([FromRoute] int productId)
    {
        var handler = GetRequestHandler<DeleteProductRequestHandler>();
        var result = handler.Handle(productId);
        return Ok(result);
    }

    [HttpPost]
    [Route("/api/product/{productId:int}/image")]
    public IActionResult PostImage([FromRoute] int productId, PostProductImageRequest request)
    {
        var handler = GetRequestHandler<PostProductImageRequestHandler>();
        var result = handler.Handle(productId, request);
        return Ok(result);
    }

    [HttpGet]
    [Route("/api/product/{productId:int}/image/primary")]
    public IActionResult GetPrimaryImage([FromRoute] int productId)
    {
        var handler = GetRequestHandler<GetProductPrimaryImageRequestHandler>();
        var result = handler.Handle(productId);
        return Ok(result);
    }
}