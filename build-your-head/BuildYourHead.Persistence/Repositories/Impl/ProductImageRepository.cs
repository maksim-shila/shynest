using System.Collections.Immutable;
using BuildYourHead.Persistence.Entities;
using BuildYourHead.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Persistence.Repositories.Impl;

internal class ProductImageRepository : RepositoryBase<ProductImageEntity, int>, IProductImageRepository
{
    public ProductImageRepository(DbContext context) : base(context)
    {
    }

    public IList<string> GetPaths(int productId)
    {
        return DbSet
            .Where(x => x.ProductId == productId)
            .Select(x => x.ImagePath)
            .ToImmutableList();
    }

    public ProductImageEntity? GetPrimaryImage(int productId)
    {
        return DbSet.FirstOrDefault(x => x.ProductId == productId && x.IsPrimary);
    }

    public void ResetPrimaryImage(int productId)
    {
        var entities = DbSet.Where(x => x.ProductId == productId).ToList();
        entities.ForEach(x => x.IsPrimary = false);
        DbSet.UpdateRange(entities);
    }
}