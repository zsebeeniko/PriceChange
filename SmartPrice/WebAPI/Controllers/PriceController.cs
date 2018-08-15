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
        public HttpResponseMessage Submit(MultipartDataMediaFormatter.Infrastructure.FormData product)
        {
            try
            {
                PriceDTO priceDto = new PriceDTO();
                priceDto = JsonConvert.DeserializeObject<PriceDTO>(product.Fields[0].Value);
                priceDto.DefaultValue = _uow.PriceOperations.GetExchangedValue(priceDto.PriceToConvert, "EUR");
                _uow.PriceOperations.Create(priceDto);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
