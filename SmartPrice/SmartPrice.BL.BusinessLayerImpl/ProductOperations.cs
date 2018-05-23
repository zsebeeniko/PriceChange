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
                Product_Id = product.Product_Id,
                Shop = product.Shop,
                Description = product.Description,
                Picture = product.Picture
            });
        }

        public void Delete(ProductDTO product)
        {
            Product entity = new Product();
            entity.Picture = product.Picture;
            entity.Product_Id = product.Product_Id;
            entity.Shop = product.Shop;
            entity.Description = product.Description;
            _productDataAccess.Delete(entity);
        }

        public IEnumerable<ProductDTO> Get()
        {
            return _productDataAccess.Read().
                Select(x => new ProductDTO()
                {
                    Product_Id = x.Product_Id,
                    Description = x.Description,
                    Picture = x.Picture,
                    Shop = x.Shop
                });
        }
    }
}
