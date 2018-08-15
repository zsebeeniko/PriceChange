using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.DL.EFDataLayer.Models.Mapping
{
    public class MarkerMap : EntityTypeConfiguration<Marker>
    {
        public MarkerMap()
        {
            // Primary Key
            this.HasKey(t => t.MarkerId);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(50);

            this.Property(t => t.Lattitude);
            this.Property(t => t.Longitude);

            // Table & Column Mappings
            this.ToTable("Markers");
            this.Property(t => t.MarkerId).HasColumnName("MarkerId");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Lattitude).HasColumnName("Lattitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
        }
    }
}