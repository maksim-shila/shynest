using BuildYourHead.Persistence.Configurations;
using BuildYourHead.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Persistence;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<ProductEntity> Products { get; set; } = null!;
    public DbSet<ProductImageEntity> ProductImages { get; set; } = null!;
    public DbSet<RecipeEntity> Recipes { get; set; } = null!;
    public DbSet<RecipeProductEntity> RecipeProducts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
    }
}