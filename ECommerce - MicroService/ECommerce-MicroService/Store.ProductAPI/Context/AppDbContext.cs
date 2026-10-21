using Microsoft.EntityFrameworkCore;
using Store.ProductAPI.Models;

namespace Store.ProductAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Category>? Categories  { get; set; }

        //FLUENT API

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasKey(c => c.CategoryId);
            mb.Entity<Category>().Property(c => c.CategoryId).ValueGeneratedOnAdd();
            mb.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);

            mb.Entity<Product>().HasKey(p => p.Id);
            mb.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
            mb.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(200);
            mb.Entity<Product>().Property(p => p.Price).IsRequired().HasPrecision(10, 2);
            mb.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(255);
            mb.Entity<Product>().Property(p => p.Stock).IsRequired();
            mb.Entity<Product>().Property(p => p.ImageURL).IsRequired(false);
            mb.Entity<Product>()
                .HasOne<Category>(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            // Seed Data
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Material Escolar" },               
                new Category { CategoryId = 3, Name = "Acessórios" },
                 new Category {  CategoryId = 2, Name = "Eletrônicos" }
            );
            mb.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Caderno",
                    Price = 19.99M,
                    Description = "Caderno universitário capa dura",
                    Stock = 100,
                    ImageURL = "caderno.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Caneta",
                    Price = 2.99M,
                    Description = "Caneta esferográfica azul",
                    Stock = 500,
                    ImageURL = "caneta.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Notebook",
                    Price = 2500.00M,
                    Description = "Notebook com 16GB RAM e 512GB SSD",
                    Stock = 50,
                    ImageURL = "notebook.jpg",
                    CategoryId = 2
                }
            );
        }
    }
}
