using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public partial class Marker:IEntity
    {
        public Marker() { }

        public int MarkerId { get; set; }
        public string Title { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
