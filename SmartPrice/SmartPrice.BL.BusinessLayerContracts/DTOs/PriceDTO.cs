using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerContracts.DTOs
{
    public class PriceDTO
    {
        public int Price_Type { get; set; }
        public string Description { get; set; }
        public string Signiture { get; set; }
    }
}
