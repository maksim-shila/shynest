namespace BuildYourHead.Api.Controllers.RecipeProducts.Requests
{
    public class PutRecipeProductsRequest
    {
        public IList<int> ProductsIds { get; set; } = new List<int>();
    }
}
