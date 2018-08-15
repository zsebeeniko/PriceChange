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
            this.HasKey(t => t.PriceId);

            // Properties
            this.Property(t => t.PriceToConvert)
                .HasMaxLength(50);

            this.Property(t => t.FromCurrency)
                .HasMaxLength(50);

            this.Property(t => t.ToCurrency)
                .HasMaxLength(50);

            this.Property(t => t.ExchangedValue);

            this.Property(t => t.DefaultValue);

            this.Property(t => t.Shop)
                .HasMaxLength(50);

            this.Property(t => t.Date);

            this.Property(t => t.PRODUCT_ID);
            // Table & Column Mappings
            this.ToTable("Prices");
            this.Property(t => t.PriceId).HasColumnName("PriceId");
            this.Property(t => t.PriceToConvert).HasColumnName("PriceToConvert");
            this.Property(t => t.FromCurrency).HasColumnName("FromCurrency");
            this.Property(t => t.ToCurrency).HasColumnName("ToCurrency");
            this.Property(t => t.ExchangedValue).HasColumnName("ExchangedValue");
            this.Property(t => t.DefaultValue).HasColumnName("DefaultValue");
            this.Property(t => t.Shop).HasColumnName("Shop");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.PRODUCT_ID).HasColumnName("PRODUCT_ID");
            
        }
    }
}
