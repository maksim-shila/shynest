using BuildYourHead.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildYourHead.Persistence.Configurations
{
    public class RecipeProductConfiguration : IEntityTypeConfiguration<RecipeProductEntity>
    {
        public void Configure(EntityTypeBuilder<RecipeProductEntity> builder)
        {
            builder.ToTable("RecipeProduct");

            builder.HasKey(rp => new { rp.RecipeId, rp.ProductId });

            builder.Property(d => d.ProductId).HasColumnName("ProductId");
            builder.Property(d => d.RecipeId).HasColumnName("RecipeId");

            builder
                .HasOne(rp => rp.Product)
                .WithMany(p => p.RecipeProducts)
                .HasForeignKey(rp => rp.ProductId);
            builder
                .HasOne(rp => rp.Recipe)
                .WithMany(p => p.RecipeProducts)
                .HasForeignKey(rp => rp.RecipeId);
        }
    }
}
