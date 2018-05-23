using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public class ProductPrice : IEntity
    {
        public int Product_Id { get; set; }
        public int Price_Type { get; set; }
        public decimal Value { get; set; }
    }
}
