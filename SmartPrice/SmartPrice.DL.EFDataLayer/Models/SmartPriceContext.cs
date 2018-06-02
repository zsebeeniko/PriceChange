using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SmartPrice.DL.DataLayerContract.Entities;
using SmartPrice.DL.EFDataLayer.Models.Mapping;

namespace SmartPrice.DL.EFDataLayer.Models
{
    public partial class SmartPriceContext : DbContext
    {
        static SmartPriceContext()
        {
            Database.SetInitializer<SmartPriceContext>(null);
        }

        public SmartPriceContext()
            : base("Name=SmartPriceContext")
        {
        }

        public DbSet<Price> Prices { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PriceMap());
            modelBuilder.Configurations.Add(new ProductPriceMap());
            modelBuilder.Configurations.Add(new ProductMap());
        }
    }
}
