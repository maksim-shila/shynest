using Microsoft.EntityFrameworkCore;

namespace BuildYourHead.Infrastructure.ImageStorage.Db
{
    internal class ImageDbContext : DbContext
    {
        private readonly string _connectionString;

        public ImageDbContext(string connectionString) : base()
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var imageTypeBuilder = builder.Entity<Image>();
            imageTypeBuilder.ToTable("Images");
            imageTypeBuilder.HasKey("Id");
            imageTypeBuilder.Property(x => x.Path).HasColumnName("Path");
            imageTypeBuilder.Property(x => x.Content).HasColumnName("Content");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql(_connectionString, new MySqlServerVersion("8.0.32"));
        }
    }
}
