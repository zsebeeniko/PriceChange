using SmartPrice.DL.DataLayerContract.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SmartPrice.DL.EFDataLayer.Models.Mapping
{
    public class ProductPriceMap : EntityTypeConfiguration<ProductPrice>
    {
        public ProductPriceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PRODUCT_ID, t.PRICE_TYPE });

            // Properties
            this.Property(t => t.PRODUCT_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PRICE_TYPE)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ProductPrice");
            this.Property(t => t.PRODUCT_ID).HasColumnName("PRODUCT_ID");
            this.Property(t => t.PRICE_TYPE).HasColumnName("PRICE_TYPE");
            this.Property(t => t.VALUE).HasColumnName("VALUE");

            // Relationships
            this.HasRequired(t => t.Price)
                .WithMany(t => t.ProductPrices)
                .HasForeignKey(d => d.PRICE_TYPE);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.ProductPrices)
                .HasForeignKey(d => d.PRODUCT_ID);

        }
    }
}
