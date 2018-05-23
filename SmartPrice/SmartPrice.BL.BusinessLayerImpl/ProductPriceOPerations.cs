using System.Collections.Generic;
using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using SmartPrice.DL.DataLayerContract;
using SmartPrice.DL.DataLayerContract.Entities;
using System.Linq;

namespace SmartPrice.BL.BusinessLayerImpl
{
    public class ProductPriceOperations : IProductPriceOperations
    {
        IDataAccess<ProductPrice> _productPriceDataAccess;
        IDataAccess<Product> _productDataAccess;
        IDataAccess<Price> _priceDataAccess;

        public ProductPriceOperations(IDataAccess<ProductPrice> productPriceDataAccess,
                                      IDataAccess<Product> productDataAccess,
                                      IDataAccess<Price> priceDataAccess)
        {
            _priceDataAccess = priceDataAccess;
            _productDataAccess = productDataAccess;
            _productPriceDataAccess = productPriceDataAccess;
        }


        public void Create(ProductPriceDTO productPrice)
        {
            var product = _productDataAccess.Read().Single(x => x.Product_Id == productPrice.Product_Id);
            var price = _priceDataAccess.Read().Single(x => x.Price_Type == productPrice.Product_Type);

            _productPriceDataAccess.Add(new ProductPrice()
            {
                Product_Id = product.Product_Id,
                Price_Type = price.Price_Type,
                Value = productPrice.Value
            });
        }

        public IEnumerable<ProductPriceDTO> Get()
        {
            throw new System.NotImplementedException();
        }
    }
}
