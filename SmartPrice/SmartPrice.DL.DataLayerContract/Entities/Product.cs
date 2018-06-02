using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public partial class Product : IEntity
    {
        public Product()
        {
            this.ProductPrices = new List<ProductPrice>();
        }

        public int PRODUCT_ID { get; set; }
        public string SHOP { get; set; }
        public string DESCRIPTION { get; set; }
        public byte[] PICTURE { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
