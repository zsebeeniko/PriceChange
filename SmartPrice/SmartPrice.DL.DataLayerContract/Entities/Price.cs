using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public partial class Price :IEntity
    {
        public Price()
        {
            this.ProductPrices = new List<ProductPrice>();
        }

        public int PRICE_TYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string SIGNITURE { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }
    }
}
