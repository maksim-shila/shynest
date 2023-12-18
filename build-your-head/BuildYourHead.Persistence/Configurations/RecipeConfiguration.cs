using BuildYourHead.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildYourHead.Persistence.Configurations
{
    public class RecipeConfiguration : IEntityTypeConfiguration<RecipeEntity>
    {
        public void Configure(EntityTypeBuilder<RecipeEntity> builder)
        {
            builder.ToTable("Recipe");

            builder.Property(d => d.Id).HasColumnName("Id");
            builder.Property(d => d.Name).HasColumnName("Name");
            builder.Property(d => d.Description).HasColumnName("Description");

            builder
                .HasMany(d => d.Products)
                .WithMany(p => p.Recipes)
                .UsingEntity<RecipeProductEntity>();
        }
    }
}
