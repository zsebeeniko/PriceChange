using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.DL.EFDataLayer.Models.Mapping
{
    public class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            // Primary Key
            this.HasKey(t => t.PicturePathId);

            // Table & Column Mappings
            this.ToTable("Pictures");
            this.Property(t => t.PicturePathId).HasColumnName("PicturePathId");
        }
    }
}