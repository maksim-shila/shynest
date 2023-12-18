using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Mappers.Interfaces;
using BuildYourHead.Persistence.Entities;

namespace BuildYourHead.Application.Mappers.Impl
{
    public class RecipeMapper : IRecipeMapper
    {
        public RecipeDto ToDto(RecipeEntity entity)
        {
            return new RecipeDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
        }

        public RecipeEntity ToEntity(RecipeDto dto)
        {
            return new RecipeEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
            };
        }
    }
}
