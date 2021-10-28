using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STApi.Data
{
    public partial class DataContext : DbContext
    {
        public DataContext() :
            base()
        {
            OnCreated();

        }
        public DataContext(DbContextOptions<DataContext> options) :
            base(options)
        {
            OnCreated();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null))
                CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.UseMySQL("server=localhost;database=library;user=user;password=password");
        }
    
        public DbSet<Product> Products { get; set; }
        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.ProductName).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Path).IsRequired();
            });
        }
        partial void OnCreated();
    }
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Path { get; set; }
        public decimal Price { get; set; }

    }
}
