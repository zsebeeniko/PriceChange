using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private IUnitOfWork _uow;

        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        [HttpGet]
        public IEnumerable<ProductDTO> GetProducts()
        {
            var list = _uow.ProductOperations.Get().ToList();
            return list;
        }


        [HttpPost]
        public HttpResponseMessage Submit(MultipartDataMediaFormatter.Infrastructure.FormData product)
        {
            try
            {
                ProductDTO productDto = new ProductDTO();
                productDto = JsonConvert.DeserializeObject<ProductDTO>(product.Fields[0].Value);
                _uow.ProductOperations.Create(productDto);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        public IHttpActionResult Save(string Image)
        {
            try
            {
                ProductDTO product = new ProductDTO();
                product.Shop = "ShopTest";
                var image = Image;
                product.Description = "DescriptionTest";
                _uow.ProductOperations.Create(product);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }
    }
}
