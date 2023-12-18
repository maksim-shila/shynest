using BuildYourHead.Application.Dto;
using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Application.Mappers.Interfaces
{
    public interface IProductMapper : IMapper<ProductDto, ProductEntity>
    {
    }
}
