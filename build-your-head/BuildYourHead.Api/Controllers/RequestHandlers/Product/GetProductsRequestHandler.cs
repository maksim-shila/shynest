using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.RequestHandlers.Product;

public class GetProductsRequestHandler : IRequestHandler
{
    private readonly IProductService _productService;

    public GetProductsRequestHandler(IProductService productService)
    {
        _productService = productService;
    }

    public IList<ProductDto> Handle()
    {
        return _productService.GetAll();
    }
}