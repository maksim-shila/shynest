using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.RequestHandlers.Product;

public class DeleteProductRequestHandler : IRequestHandler
{
    private readonly IProductService _productService;

    public DeleteProductRequestHandler(IProductService productService)
    {
        _productService = productService;
    }

    public string Handle(int id)
    {
        _productService.Delete(id);
        return $"Product {id} successfully removed";
    }
}