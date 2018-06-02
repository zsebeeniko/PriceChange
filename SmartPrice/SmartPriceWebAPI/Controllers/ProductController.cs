using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartPriceWebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private IUnitOfWork _uow;

        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Route("Product/Products")]
        public IEnumerable<ProductDTO> GetProducts()
        {
            return _uow.ProductOperations.Get();
            //////var product = new List<ProductDTO>();
            //////var product1 = new ProductDTO();
            //////product1.Description = "Description";
            //////product1.Shop = "Shop";
            //////product1.Product_Id = 2;
            //////product1.Picture = null;
            //////product.Add(product1);

            //////return product;
        }
    }
}
