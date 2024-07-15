using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Exceptions;
using BuildYourHead.Application.Mappers.Interfaces;
using BuildYourHead.Persistence;
using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Application.Services.Impl;

public class ProductService : IProductService
{
    private readonly IProductMapper _mapper;
    private readonly IImageService _imageService;
    private readonly IUnitOfWork _uow;

    public ProductService(IProductMapper mapper, IImageService imageService, IUnitOfWork uow)
    {
        _mapper = mapper;
        _imageService = imageService;
        _uow = uow;
    }

    public IList<ProductDto> GetAll()
    {
        var entities = _uow.Products.Get();
        return _mapper.ToDtos(entities);
    }

    public ProductDto Add(ProductDto product)
    {
        var entity = _mapper.ToEntity(product);
        var created = _uow.Products.Create(entity);
        _uow.Save();
        return _mapper.ToDto(created);
    }

    public ProductDto Update(ProductDto product)
    {
        var entity = _mapper.ToEntity(product);
        _uow.Products.Update(entity);
        _uow.Save();
        return _mapper.ToDto(entity);
    }

    public ProductDto Get(int id)
    {
        var entity = _uow.Products.Get(id);
        if (entity == null)
        {
            throw new NotFoundException($"Product with id {id} not found.");
        }

        return _mapper.ToDto(entity);
    }

    public void AttachImage(int productId, string imagePath, bool primary)
    {
        var entity = new ProductImageEntity {ImagePath = imagePath, ProductId = productId, IsPrimary = primary};
        if (primary)
        {
            _uow.ProductImages.ResetPrimaryImage(productId);
        }

        _uow.ProductImages.Create(entity);
        _uow.Save();
    }

    public void Delete(int id)
    {
        var entity = _uow.Products.Get(id);
        if (entity == null)
        {
            throw new NotFoundException($"Product with id {id} not found.");
        }

        var imagesPaths = _uow.ProductImages.GetPaths(id);
        _uow.Products.Delete(entity);
        _uow.Save();

        _imageService.Delete(imagesPaths);
    }

    public string? GetPrimaryImage(int id)
    {
        var productImageEntity = _uow.ProductImages.GetPrimaryImage(id);
        if (productImageEntity == null)
        {
            return null;
        }

        var path = productImageEntity.ImagePath;
        return _imageService.Get(path);
    }
}