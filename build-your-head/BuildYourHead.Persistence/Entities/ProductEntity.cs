namespace BuildYourHead.Persistence.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public double Proteins { get; set; }
        public double Carbohydrates { get; set; }
        public double Fats { get; set; }
        public double Nutrition { get; set; }

        public virtual List<ProductImageEntity> Images { get; set; } = new List<ProductImageEntity>();
        public virtual List<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
        public virtual List<RecipeProductEntity> RecipeProducts { get; set; } = new List<RecipeProductEntity>();
    }
}
