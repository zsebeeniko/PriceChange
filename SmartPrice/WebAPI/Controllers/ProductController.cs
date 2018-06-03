﻿using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Collections.Generic;
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
        
        public IEnumerable<ProductDTO> GetProducts()
        {
            var list = _uow.ProductOperations.Get().ToList();
            return list;
        }

        [HttpPost]
        public IHttpActionResult Submit(ProductDTO product)
        {
            try
            {
                _uow.ProductOperations.Create(product);
                _uow.SaveChanges();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }
    }
}