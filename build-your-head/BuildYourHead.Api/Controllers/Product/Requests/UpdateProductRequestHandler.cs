using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class UpdateProductRequestHandler : IRequestHandler
    {
        private readonly IProductService _productService;

        public UpdateProductRequestHandler(IProductService productService)
        {
            _productService = productService;
        }

        public ProductDto Handle(int id, UpdateProductRequest request)
        {
            if (id <= 0)
            {
                throw new ValidationException("Product id should be greater than zero");
            }
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ValidationException("Product name should be present");
            }
            if (request.Carbohydrates < 0 || request.Proteins < 0 || request.Fats < 0 || request.Nutrition < 0)
            {
                throw new ValidationException("Macronutrients values should be greater or equal to zero.");
            }

            var dto = new ProductDto
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Proteins = request.Proteins,
                Carbohydrates = request.Carbohydrates,
                Fats = request.Fats,
                Nutrition = request.Nutrition
            };

            return _productService.Update(dto);
        }
    }
}
