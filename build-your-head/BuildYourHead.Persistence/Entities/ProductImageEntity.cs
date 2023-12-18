namespace BuildYourHead.Persistence.Entities
{
    public class ProductImageEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; } = null!;
        public bool IsPrimary { get; set; }

        public virtual ProductEntity Product { get; set; } = null!;
    }
}
