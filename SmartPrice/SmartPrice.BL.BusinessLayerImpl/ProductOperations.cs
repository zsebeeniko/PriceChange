using System;
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

        public int Create(ProductDTO product)
        {
            product.Product_Id = _productDataAccess.Read().Count() + 1;
            _productDataAccess.Add(new Product()
            {
                PRODUCT_ID = product.Product_Id,
                DESCRIPTION = product.Description,
                Name = product.Name
            });

            return product.Product_Id;
        }

        public void Delete(ProductDTO product)
        {
            Product entity = new Product();
            entity.PRODUCT_ID = product.Product_Id;
            entity.DESCRIPTION = product.Description;
            entity.Name = product.Name;
            _productDataAccess.Delete(entity);
        }

        public IEnumerable<ProductDTO> Get()
        {
            return _productDataAccess.Read().
                Select(x => new ProductDTO()
                {
                    Product_Id = x.PRODUCT_ID,
                    Description = x.DESCRIPTION,
                    Name = x.Name
                });
        }

        public List<String> GetNames()
        {
            return _productDataAccess.Read().Select(x => x.Name).ToList();
        }
    }
}
