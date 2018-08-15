using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public partial class Product : IEntity
    {
        public Product()
        {
        }

        public int PRODUCT_ID { get; set; }
        public string Name { get; set; }
        public string DESCRIPTION { get; set; }

        public ICollection<Price> prices { get; set; }
    }
}
