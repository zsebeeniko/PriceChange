using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerContracts.DTOs
{
    public class PriceDTO
    {
        public int Price_Id { get; set; }
        public string PriceToConvert { get; set; }
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal ExchangedValue { get; set; }
        public decimal DefaultValue { get; set; }
        public string Shop { get; set; }
        public int Product_Id { get; set; }
        public int PicturePathId { get; set; }
        public DateTime Date { get; set; }
        public ProductDTO product { get; set; }
    }
}
