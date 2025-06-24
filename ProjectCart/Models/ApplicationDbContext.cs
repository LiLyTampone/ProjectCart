using Microsoft.EntityFrameworkCore;
using ProjectCart.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ProjectCart.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed dữ liệu mẫu
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Bánh quy", Price = 17500, ImageUrl = "/image/Banh_quy.jpg" },
                new Product { Id = 2, Name = "Dầu ăn", Price =65000 , ImageUrl = "/image/Dau_an.jpg" },
                new Product { Id = 3, Name = "Mì trộn", Price = 8000, ImageUrl = "/image/Mi_tron.jng" },
                new Product { Id = 4, Name = "Tương Cà", Price = 12800, ImageUrl = "/image/tuongca.png" }
            );
        }
    }
}
