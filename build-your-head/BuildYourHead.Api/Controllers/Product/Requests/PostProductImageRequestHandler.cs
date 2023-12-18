using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class PostProductImageRequestHandler : IRequestHandler
    {
        private readonly IProductService _productService;

        public PostProductImageRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public string Handle(int id, PostProductImageRequest request)
        {
            if (id <= 0)
            {
                throw new ValidationException("Product id should be greater than zero");
            }
            if (string.IsNullOrWhiteSpace(request.ImagePath))
            {
                throw new ValidationException("Image path should be present");
            }

            _productService.AttachImage(id, request.ImagePath, request.Primary);
            return "Image attached to product";
        }
    }
}
