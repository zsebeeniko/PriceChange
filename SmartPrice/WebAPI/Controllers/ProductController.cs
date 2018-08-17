using Newtonsoft.Json;
using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Collections.Generic;
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
        //public IHttpActionResult GetProducts()
        //{
        //    var list = _uow.ProductOperations.Get().ToList();
        //    return Ok(list);
        //}


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
