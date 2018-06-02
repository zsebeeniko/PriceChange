using System.Collections.Generic;
using System.Linq;
using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using SmartPrice.DL.DataLayerContract;
using SmartPrice.DL.DataLayerContract.Entities;

namespace SmartPrice.BL.BusinessLayerImpl
{
    public class ProductOperations : IProductOperations
    {
        private IDataAccess<Product> _productDataAccess;

        public ProductOperations(IDataAccess<Product> productDataAccess)
        {
            _productDataAccess = productDataAccess;
        }
        public void Create(ProductDTO product)
        {
            _productDataAccess.Add(new Product()
            {
                PRODUCT_ID = product.Product_Id,
                SHOP = product.Shop,
                DESCRIPTION = product.Description,
                PICTURE = product.Picture
            });
        }

        public void Delete(ProductDTO product)
        {
            Product entity = new Product();
            entity.PICTURE = product.Picture;
            entity.PRODUCT_ID = product.Product_Id;
            entity.SHOP = product.Shop;
            entity.DESCRIPTION = product.Description;
            _productDataAccess.Delete(entity);
        }

        public IEnumerable<ProductDTO> Get()
        {
            return _productDataAccess.Read().
                Select(x => new ProductDTO()
                {
                    Product_Id = x.PRODUCT_ID,
                    Description = x.DESCRIPTION,
                    Picture = x.PICTURE,
                    Shop = x.SHOP
                });
        }
    }
}
