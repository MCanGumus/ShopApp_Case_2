using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace ShopApp.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } 
        public DbSet<User> Users { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.Property(p => p.Category).IsRequired();
                entity.Property(p => p.StockQuantity).IsRequired();
                entity.Property(p => p.ImageUrl).HasMaxLength(500);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Username).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Email).HasMaxLength(100);
                entity.Property(p => p.PasswordHash).HasMaxLength(250);
            });
        }
    }
}
