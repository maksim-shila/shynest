﻿namespace BuildYourHead.Api.Controllers.Requests.RecipeProduct;

public class PutRecipeProductsRequest
{
    public IList<int> ProductsIds { get; set; } = new List<int>();
}