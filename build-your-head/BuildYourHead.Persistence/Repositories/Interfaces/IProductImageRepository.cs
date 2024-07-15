using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Persistence.Repositories.Interfaces;

public interface IProductImageRepository : IRepository<ProductImageEntity, int>
{
    IList<string> GetPaths(int productId);
    ProductImageEntity? GetPrimaryImage(int productId);
    void ResetPrimaryImage(int productId);
}