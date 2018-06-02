using SmartPrice.DL.DataLayerContract.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SmartPrice.DL.EFDataLayer.Models.Mapping
{
    public class PriceMap : EntityTypeConfiguration<Price>
    {
        public PriceMap()
        {
            // Primary Key
            this.HasKey(t => t.PRICE_TYPE);

            // Properties
            this.Property(t => t.DESCRIPTION)
                .HasMaxLength(50);

            this.Property(t => t.SIGNITURE)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Prices");
            this.Property(t => t.PRICE_TYPE).HasColumnName("PRICE_TYPE");
            this.Property(t => t.DESCRIPTION).HasColumnName("DESCRIPTION");
            this.Property(t => t.SIGNITURE).HasColumnName("SIGNITURE");
        }
    }
}
