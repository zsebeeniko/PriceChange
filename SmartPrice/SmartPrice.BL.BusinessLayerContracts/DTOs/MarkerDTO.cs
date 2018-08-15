using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.BL.BusinessLayerContracts.DTOs
{
    public class MarkerDTO
    {
        public int MarkerId { get; set; }
        public string Title { get; set; }
        public decimal Lattitude { get; set; }
        public decimal Lingitude { get; set; }
    }
}
