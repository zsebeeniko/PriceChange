using Newtonsoft.Json;
using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class PriceController : ApiController
    {
        private IUnitOfWork _uow;

        public PriceController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpPost]
        public HttpResponseMessage Submit(MultipartDataMediaFormatter.Infrastructure.FormData price)
        {
            try
            {
                PriceDTO priceDto = new PriceDTO();
                priceDto = JsonConvert.DeserializeObject<PriceDTO>(price.Fields[0].Value);
                priceDto.DefaultValue = _uow.PriceOperations.GetExchangedValue(priceDto.PriceToConvert, "EUR");
                if (priceDto.Product_Id == -1)
                {
                    priceDto.product = _uow.ProductOperations.GetProductByName(priceDto.product.Name);
                    priceDto.Product_Id = priceDto.product.Product_Id;
                }
                else
                {
                    priceDto.Product_Id = _uow.ProductOperations.LastProductId();
                }
                _uow.PriceOperations.Create(priceDto);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        public IHttpActionResult GetPrices()
        {
            List<PriceDTO> list = _uow.PriceOperations.Get().ToList();
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult GetBestPrice(string name)
        {
            PriceDTO price = new PriceDTO();
            price = _uow.PriceOperations.GetBestPrice(name);
            return Ok(price);
        }

        [HttpGet]
        public IHttpActionResult GetFilteredProducts(string date)
        {
            List<PriceDTO> list = _uow.PriceOperations.GetFilteredProducts(date);
            return Ok(list);
        }
    }
}
