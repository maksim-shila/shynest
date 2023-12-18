using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class GetProductPrimaryImageRequestHandler : IRequestHandler
    {
        private readonly IProductService _productService;

        public GetProductPrimaryImageRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public string? Handle(int id)
        {
            if (id <= 0)
            {
                throw new ValidationException("Product id should be greater than zero");
            }

            return _productService.GetPrimaryImage(id);
        }
    }
}
