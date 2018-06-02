using System.Collections.Generic;
using System.Linq;
using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using SmartPrice.DL.DataLayerContract;
using SmartPrice.DL.DataLayerContract.Entities;

namespace SmartPrice.BL.BusinessLayerImpl
{
    public class PriceOperations : IPriceOperations
    {
        private IDataAccess<Price> _priceDataAccess;

        public PriceOperations(IDataAccess<Price> priceDataAccess)
        {
            _priceDataAccess = priceDataAccess;
        }

        public IEnumerable<PriceDTO> Get()
        {
            return _priceDataAccess.Read().
                Select(x => new PriceDTO()
                {
                    Price_Type = x.PRICE_TYPE,
                    Description = x.DESCRIPTION,
                    Signiture = x.SIGNITURE
                });
        }
    }
}
