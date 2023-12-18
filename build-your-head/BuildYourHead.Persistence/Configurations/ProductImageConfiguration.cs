using BuildYourHead.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildYourHead.Persistence.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImageEntity>
    {
        public void Configure(EntityTypeBuilder<ProductImageEntity> builder)
        {
            builder.ToTable("ProductImage");

            builder.Property(d => d.Id).HasColumnName("Id");
            builder.Property(d => d.ProductId).HasColumnName("ProductId");
            builder.Property(d => d.ImagePath).HasColumnName("ImagePath");
            builder.Property(d => d.IsPrimary).HasColumnName("IsPrimary");

            builder
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId);
        }
    }
}
