using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class GetPrimaryImageRequestHandler : IRequestHandler
    {
        private readonly IProductService _productService;

        public GetPrimaryImageRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public string? Handle(int productId)
        {
            if (productId <= 0)
            {
                throw new ValidationException("Product id should be greater than zero");
            }

            return _productService.GetPrimaryImage(productId);
        }
    }
}
