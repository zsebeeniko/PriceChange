using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice.DL.DataLayerContract;
using SmartPrice.DL.DataLayerContract.Entities;
using SmartPrice.DL.EFDataLayer.Models;

namespace SmartPrice.DL.EFDataLayer
{
    public class EFRepository : IRepository
    {
        private SmartPriceContext _ctx;
        private IDataAccess<DataLayerContract.Entities.Product> _productDataAccess;
        private IDataAccess<DataLayerContract.Entities.Price> _priceDataAccess;
        private IDataAccess<DataLayerContract.Entities.Marker> _markerDataAccess;
        private IDataAccess<DataLayerContract.Entities.Picture> _pictureDataAccess;

        public EFRepository(SmartPriceContext ctx)
        {
            _ctx = ctx;
        }


        public IDataAccess<DataLayerContract.Entities.Product> ProductRepository
        {
            get
            {
                if (_productDataAccess == null)
                {
                    _productDataAccess = new EFDataAccess<DataLayerContract.Entities.Product>(_ctx);
                }

                return _productDataAccess;
            }
        }

        public IDataAccess<DataLayerContract.Entities.Price> PriceRepository
        {
            get
            {
                if (_priceDataAccess == null)
                {
                    _priceDataAccess = new EFDataAccess<DataLayerContract.Entities.Price>(_ctx);
                }

                return _priceDataAccess;
            }
        }

        public IDataAccess<DataLayerContract.Entities.Marker> MarkerRepository
        {
            get
            {
                if (_markerDataAccess == null)
                {
                    _markerDataAccess = new EFDataAccess<DataLayerContract.Entities.Marker>(_ctx);
                }

                return _markerDataAccess;
            }
        }

        public IDataAccess<DataLayerContract.Entities.Picture> PictureRepository
        {
            get
            {
                if (_pictureDataAccess == null)
                {
                    _pictureDataAccess = new EFDataAccess<DataLayerContract.Entities.Picture>(_ctx);
                }

                return _pictureDataAccess;
            }
        }

        public void SaveChanges()
        {
            try
            {
                _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}
