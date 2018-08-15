using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public partial class Price :IEntity
    {
        public Price()
        {
        }

        public int PriceId { get; set; }
        public string PriceToConvert { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal ExchangedValue { get; set; }
        public decimal DefaultValue { get; set; }
        public string Shop { get; set; }
        public DateTime Date { get; set; }
        public int PicturePathId { get; set; }
        public int PRODUCT_ID { get; set; }

        public Product product { get; set; }
    }
}
