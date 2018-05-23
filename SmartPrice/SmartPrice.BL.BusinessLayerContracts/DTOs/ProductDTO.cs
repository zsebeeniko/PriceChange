using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerContracts.DTOs
{
    public class ProductDTO
    {
        public int Product_Id { get; set; }
        public string Shop { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
