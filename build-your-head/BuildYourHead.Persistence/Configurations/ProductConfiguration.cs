using BuildYourHead.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildYourHead.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Product");

            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.Name).HasColumnName("Name");
            builder.Property(p => p.Description).HasColumnName("Description");
            builder.Property(p => p.Proteins).HasColumnName("Proteins");
            builder.Property(p => p.Carbohydrates).HasColumnName("Carbohydrates");
            builder.Property(p => p.Fats).HasColumnName("Fats");
            builder.Property(p => p.Nutrition).HasColumnName("Nutrition");

            builder
                .HasMany(p => p.Recipes)
                .WithMany(d => d.Products)
                .UsingEntity<RecipeProductEntity>();
        }
    }
}
