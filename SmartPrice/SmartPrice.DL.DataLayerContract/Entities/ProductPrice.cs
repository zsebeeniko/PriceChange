using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public partial class ProductPrice :IEntity
    {
        public int PRODUCT_ID { get; set; }
        public int PRICE_TYPE { get; set; }
        public decimal VALUE { get; set; }
        public virtual Price Price { get; set; }
        public virtual Product Product { get; set; }
    }
}
