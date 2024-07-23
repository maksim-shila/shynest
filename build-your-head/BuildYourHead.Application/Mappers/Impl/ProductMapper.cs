using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Mappers.Interfaces;
using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Application.Mappers.Impl;

public class ProductMapper : IProductMapper
{
    public ProductEntity ToEntity(ProductDto dto)
    {
        return new ProductEntity
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Proteins = dto.Proteins,
            Fats = dto.Fats,
            Carbohydrates = dto.Carbohydrates,
            Nutrition = dto.Nutrition
        };
    }

    public ProductDto ToDto(ProductEntity entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Proteins = entity.Proteins,
            Fats = entity.Fats,
            Carbohydrates = entity.Carbohydrates,
            Nutrition = entity.Nutrition
        };
    }
}