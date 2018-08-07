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
using SmartPrice.Cluster;

namespace WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        private IUnitOfWork _uow;
        private DocumentCollection docCollection;

        private string txtDoc1 = "Huf, huf, forint, ft, FT";
        private string txtDoc2 = "euro, eur, eu,EU,EURO, Eu, euro, euro, euro, euro, euro, EuRo, EUro, EURRo, Euro";
        private string txtDoc3 = "ron, RON, leu, LEU, lei, Lei, ro, RO";
        private string txtDoc4 = "usd, USD, dollar, dolar. Dollar, Dolar, Doll, Dol";

        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
            docCollection = new DocumentCollection() { DocumentList = new List<string>() };
        }

        [HttpGet]
        public string GetExchangedValue(string priceToConvert)
        {
            var exchangedValue = "";
            
            docCollection.DocumentList.Add(txtDoc1);
            docCollection.DocumentList.Add(txtDoc2);
            docCollection.DocumentList.Add(txtDoc3);
            docCollection.DocumentList.Add(txtDoc4);
            docCollection.DocumentList.Add(priceToConvert);

            List<DocumentVector> vSpace = VectorSpaceModel.ProcessDocumentCollection(docCollection);
            int totalIteration = 0;
            List<Centroid> resultSet = DocumnetClustering.PrepareDocumentCluster(4, vSpace, ref totalIteration);
            string msg = string.Empty;
            int count = 1;
            foreach (Centroid c in resultSet)
            {
                msg += String.Format("------------------------------[ CLUSTER {0} ]-----------------------------{1}", count, System.Environment.NewLine);

                foreach (DocumentVector document in c.GroupedDocument)
                {
                    msg += document.Content + System.Environment.NewLine;
                    if (c.GroupedDocument.Count > 1)
                    {
                        msg += String.Format("{0}-------------------------------------------------------------------------------{0}", System.Environment.NewLine);
                    }
                }
                msg += "-------------------------------------------------------------------------------" + System.Environment.NewLine;
                count++;
            }

            exchangedValue = msg;
            return exchangedValue;
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
