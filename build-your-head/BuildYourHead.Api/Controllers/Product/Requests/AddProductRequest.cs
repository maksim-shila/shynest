namespace BuildYourHead.Api.Controllers.Product.Requests
{
    public class AddProductRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Proteins { get; set; }
        public double Carbohydrates { get; set; }
        public double Fats { get; set; }
        public double Nutrition { get; set; }
    }
}
