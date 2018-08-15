using SmartPrice.DL.DataLayerContract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPrice.DL.DataLayerContract
{
    public interface IRepository
    {
        IDataAccess<Product> ProductRepository { get; }
        IDataAccess<Price> PriceRepository { get; }
        IDataAccess<Marker> MarkerRepository { get; }
        IDataAccess<Picture> PictureRepository { get; }

        void SaveChanges();
    }
}
