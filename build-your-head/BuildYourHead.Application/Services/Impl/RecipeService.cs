using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Exceptions;
using BuildYourHead.Application.Mappers.Interfaces;
using BuildYourHead.Persistence;

namespace BuildYourHead.Application.Services.Impl
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeMapper _recipeMapper;
        private readonly IProductMapper _productMapper;
        private readonly IUnitOfWork _uow;

        public RecipeService(IRecipeMapper mapper, IProductMapper productMapper, IUnitOfWork uow)
        {
            _recipeMapper = mapper;
            _productMapper = productMapper;
            _uow = uow;
        }

        public IList<RecipeDto> GetAll()
        {
            var entities = _uow.Recipes.Get();
            return _recipeMapper.ToDtos(entities);
        }
        public RecipeDto Get(int id)
        {
            var entity = _uow.Recipes.Get(id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Recipe with id {id} not found.");
            }

            return _recipeMapper.ToDto(entity);
        }

        public RecipeDto Add(RecipeDto recipe)
        {
            var entity = _recipeMapper.ToEntity(recipe);
            var created = _uow.Recipes.Create(entity);
            _uow.Save();
            return _recipeMapper.ToDto(created);
        }

        public RecipeDto Update(RecipeDto recipe)
        {
            var entity = _recipeMapper.ToEntity(recipe);
            _uow.Recipes.Update(entity);
            _uow.Save();
            return _recipeMapper.ToDto(entity);
        }

        public void Delete(int id)
        {
            var entity = _uow.Recipes.Get(id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Recipe with id {id} not found.");
            }
            _uow.Recipes.Delete(entity);
            _uow.Save();
        }

        public IList<ProductDto> GetProducts(int recipeId)
        {
            var productEntities = _uow.RecipeProducts.FindProductsByRecipeId(recipeId);
            return _productMapper.ToDtos(productEntities);
        }

        public void AddProducts(int recipeId, IList<int> productsIds)
        {
            var recipeEntity = _uow.Recipes.Get(recipeId);
            if (recipeEntity == null)
            {
                throw new EntityNotFoundException($"Recipe with id {recipeId} not found.");
            }
            var recipeProducts = _uow.RecipeProducts.FindProductsByRecipeId(recipeId);
            var alreadyAddedProductsIds = recipeProducts.Select(p => p.Id).Intersect(productsIds);
            if (alreadyAddedProductsIds.Any())
            {
                throw new AlreadyExistsException("Some Products already assigned");
            }
            foreach (var productId in productsIds)
            {
                var productEntity = _uow.Products.Get(productId);
                if (productEntity == null)
                {
                    throw new EntityNotFoundException($"Product with id {productId} not exists");
                }
            }

            _uow.RecipeProducts.Add(recipeId, productsIds);
            _uow.Save();
        }

        public void DeleteRecipeProduct(int recipeId, int productId)
        {
            var recipeProduct = _uow.RecipeProducts.Get(recipeId, productId);
            if (recipeProduct == null)
            {
                throw new EntityNotFoundException($"Product with id {productId} related to recipe with id {recipeId} not found.");
            }

            _uow.RecipeProducts.Delete(recipeProduct);
            _uow.Save();
        }
    }
}
