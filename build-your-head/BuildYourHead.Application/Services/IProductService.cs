using BuildYourHead.Application.Dto;

namespace BuildYourHead.Application.Services
{
    public interface IProductService
    {
        ProductDto Get(int id);
        ProductDto Add(ProductDto product);
        ProductDto Update(ProductDto product);
        IList<ProductDto> GetAll();
        void Delete(int id);
        void AttachImage(int productId, string imagePath, bool primary);
        string? GetPrimaryImage(int id);
    }
}
