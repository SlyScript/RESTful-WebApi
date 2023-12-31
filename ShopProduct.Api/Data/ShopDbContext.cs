using Microsoft.EntityFrameworkCore;
using ShopProduct.Api.Entities;

namespace ShopProduct.Api.Data
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> opt) : base(opt)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Product 1",
                Description = "Product 1 description",
                Price = 500,
                Quantity = 100,
            },
            new Product
            {
                Id = 2,
                Name = "Product 2",
                Description = "Product 2 description",
                Price = 200,
                Quantity = 110,
            },
            new Product
            {
                Id = 3,
                Name = "Product 3",
                Description = "Product 3 description",
                Price = 50,
                Quantity = 90,
            });
        }
    }
}
