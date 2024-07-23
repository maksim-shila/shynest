using BuildYourHead.Persistence.Entities;
using BuildYourHead.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Persistence.Repositories.Impl;

internal class ProductRepository : RepositoryBase<ProductEntity, int>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }
}