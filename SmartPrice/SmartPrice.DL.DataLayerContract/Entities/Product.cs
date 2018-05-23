using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public class Product: IEntity
    {
        public int Product_Id { get; set; }
        public string Shop { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
    }
}
