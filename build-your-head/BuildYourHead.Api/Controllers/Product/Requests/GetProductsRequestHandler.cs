using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class GetProductsRequestHandler : IRequestHandler
    {
        private IProductService _productService;

        public GetProductsRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public IList<ProductDto> Handle()
        {
            return _productService.GetAll();
        }
    }
}
