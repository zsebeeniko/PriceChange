using SmartPrice.BL.BusinessLayerContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class PictureController : ApiController
    {
        private IUnitOfWork _uow;

        public PictureController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public int GetNextPathId()
        {
            int nextId;
            nextId = _uow.PriceOperations.GetNextPathId();
            return nextId;
        }
    }
}