using SmartPrice.DL.DataLayerContract.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SmartPrice.DL.EFDataLayer.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.PRODUCT_ID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Products");
            this.Property(t => t.PRODUCT_ID).HasColumnName("PRODUCT_ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
        }
    }
}
