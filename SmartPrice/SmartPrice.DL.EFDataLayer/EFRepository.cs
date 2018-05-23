using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPrice.DL.DataLayerContract;
using SmartPrice.DL.DataLayerContract.Entities;

namespace SmartPrice.DL.EFDataLayer
{
    public class EFRepository : IRepository
    {
        private SmartPriceContext _ctx;
        private IDataAccess<DataLayerContract.Entities.Product> _productDataAccess;
        private IDataAccess<DataLayerContract.Entities.Price> _priceDataAccess;
        private IDataAccess<DataLayerContract.Entities.ProductPrice> _productPriceDataAccess;

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

        public IDataAccess<DataLayerContract.Entities.ProductPrice> ProductPriceRepository
        {
            get
            {
                if (_productPriceDataAccess == null)
                {
                    _productPriceDataAccess = new EFDataAccess<DataLayerContract.Entities.ProductPrice>(_ctx);
                }

                return _productPriceDataAccess;
            }
        }

        public void SaveChanges()
        {
            _ctx.SaveChanges();
        }
    }
}
