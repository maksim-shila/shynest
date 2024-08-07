﻿using BuildYourHead.Api.Exceptions;
using BuildYourHead.Application.Dto;
using BuildYourHead.Application.Services;

namespace BuildYourHead.Api.Controllers.RequestHandlers.RecipeProduct;

public class GetRecipeProductsRequestHandler : IRequestHandler
{
    private readonly IRecipeService _recipeService;

    public GetRecipeProductsRequestHandler(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public IList<ProductDto> Handle(int recipeId)
    {
        if (recipeId <= 0)
        {
            throw new ValidationException("Recipe id should be greater than zero");
        }

        return _recipeService.GetProducts(recipeId);
    }
}