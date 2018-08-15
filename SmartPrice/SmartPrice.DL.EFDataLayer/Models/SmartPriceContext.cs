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
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PriceMap());
            modelBuilder.Configurations.Add(new MarkerMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new PictureMap());

            modelBuilder.Entity<Price>()
            .HasRequired<Product>(s => s.product)
            .WithMany(g => g.prices)
            .HasForeignKey<int>(s => s.PRODUCT_ID);
        }
    }
}
