using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice.DL.DataLayerContract.Entities;

namespace SmartPrice.DL.EFDataLayer
{
    public class SmartPriceContext : DbContext
    {
        public SmartPriceContext()
            : base("name=SmartPriceEntities")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public DbSet<Price> Prices { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
