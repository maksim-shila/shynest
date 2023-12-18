namespace BuildYourHead.Persistence.Entities
{
    public class RecipeProductEntity
    {
        public int RecipeId { get; set; }
        public int ProductId { get; set; }

        public virtual RecipeEntity Recipe { get; set; } = null!;
        public virtual ProductEntity Product { get; set; } = null!;
    }
}
