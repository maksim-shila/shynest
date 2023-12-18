using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class GetProductRequestHandler : IRequestHandler
    {
        private IProductService _productService;

        public GetProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public ProductDto Handle(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Product id should be greater than zero");
            }

            return _productService.Get(id);
        }
    }
}
