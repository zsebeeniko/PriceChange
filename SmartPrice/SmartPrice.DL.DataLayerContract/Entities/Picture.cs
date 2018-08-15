using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.DL.DataLayerContract.Entities
{
    public partial class Picture : IEntity
    {
        public Picture() { }

        public int PicturePathId { get; set; }
    }
}
