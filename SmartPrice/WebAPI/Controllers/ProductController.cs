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
        public decimal GetExchangedValue(string priceToConvert, string to_currency)
        {
            decimal exchangedValue = -1;
            exchangedValue = _uow.PriceOperations.GetExchangedValue(priceToConvert, to_currency);
            return exchangedValue;
        }

        [HttpGet]
        public string GetFromCurrency(string priceToConvert)
        {
            string from_currency = "";
            from_currency = _uow.PriceOperations.GetFromCurrency(priceToConvert);
            return from_currency;
        }

        [HttpGet]
        public List<String> GetProductNames()
        {
            List<String> names;
            names = _uow.ProductOperations.GetNames();
            return names;
        }

        //[HttpGet]
        //public IEnumerable<ProductDTO> GetProducts()
        //{
        //    var list = _uow.ProductOperations.Get().ToList();
        //    return list;
        //}


        [HttpPost]
        public int Submit(MultipartDataMediaFormatter.Infrastructure.FormData product)
        {
            int new_id = -1;
            try
            {
                ProductDTO productDto = new ProductDTO();
                productDto = JsonConvert.DeserializeObject<ProductDTO>(product.Fields[0].Value);
                new_id = _uow.ProductOperations.Create(productDto);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return new_id;
        }

        [HttpPost]
        public IHttpActionResult Save(string Image)
        {
            try
            {
                ProductDTO product = new ProductDTO();
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
