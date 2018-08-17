using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using SmartPrice.BL.BusinessLayerContracts;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using SmartPrice.Cluster;
using SmartPrice.DL.DataLayerContract;
using SmartPrice.DL.DataLayerContract.Entities;

namespace SmartPrice.BL.BusinessLayerImpl
{
    public class PriceOperations : IPriceOperations
    {
        private IDataAccess<Price> _priceDataAccess;
        private IDataAccess<Product> _productDataAccess;

        private DocumentCollection docCollection;
        private decimal value;
        private string currency;
        private string currency_path = "C:\\Users\\ZsEni\\Documents\\Disszertacio\\Projekt\\SmartPrice\\Cluster\\Text\\Currencies.txt";

        public PriceOperations(IDataAccess<Price> priceDataAccess, IDataAccess<Product> productDataAccess)
        {
            _priceDataAccess = priceDataAccess;
            _productDataAccess = productDataAccess;
            docCollection = new DocumentCollection() { DocumentList = new List<string>() };
        }

        public List<PriceDTO> GetFilteredProducts(string pDate)
        {
            DateTime date = DateTime.Parse(pDate);

            return _priceDataAccess.Read().Where(x => x.Date == date).
                    Select(x => new PriceDTO()
                    {
                        Price_Id = x.PriceId,
                        PriceToConvert = x.PriceToConvert,
                        FromCurrency = x.FromCurrency,
                        ToCurrency = x.ToCurrency,
                        ExchangedValue = x.ExchangedValue,
                        DefaultValue = x.DefaultValue,
                        Shop = x.Shop,
                        Date = x.Date,
                        Product_Id = x.PRODUCT_ID,
                        PicturePathId = x.PicturePathId,
                        product = new ProductDTO() { Description = x.product.DESCRIPTION, Name = x.product.Name, Product_Id = x.PRODUCT_ID }
                    }).ToList();
        }

        public PriceDTO GetBestPrice(string name)
        {
            decimal minvalue = _priceDataAccess.Read().Where(x => x.product.Name == name).Min(x => x.DefaultValue);
            return _priceDataAccess.Read().Where(x => x.DefaultValue == minvalue).
                    Select( x => new PriceDTO() {
                        Price_Id = x.PriceId,
                        PriceToConvert = x.PriceToConvert,
                        FromCurrency = x.FromCurrency,
                        ToCurrency = x.ToCurrency,
                        ExchangedValue = x.ExchangedValue,
                        DefaultValue = x.DefaultValue,
                        Shop = x.Shop,
                        Date = x.Date,
                        Product_Id = x.PRODUCT_ID,
                        PicturePathId = x.PicturePathId,
                        product = new ProductDTO() { Description = x.product.DESCRIPTION, Name = x.product.Name, Product_Id = x.PRODUCT_ID }
                    }).FirstOrDefault();
        }

        public void Create(PriceDTO price)
        {
            price.Price_Id = _priceDataAccess.Read().Count() + 1;
            Product entity = _productDataAccess.Read().Where(x => x.Name == price.product.Name).FirstOrDefault();
            _priceDataAccess.Add(new Price()
            {
                PriceId = price.Price_Id,
                PriceToConvert = price.PriceToConvert,
                FromCurrency = price.FromCurrency,
                ToCurrency = price.ToCurrency,
                ExchangedValue = price.ExchangedValue,
                DefaultValue = price.DefaultValue,
                Date = DateTime.Today,
                Shop = price.Shop,
                PicturePathId = price.PicturePathId,
                PRODUCT_ID = price.Product_Id,
                product = entity == null ? new Product() { DESCRIPTION = price.product.Description, PRODUCT_ID = price.Product_Id, Name = price.product.Name } :entity
            });
        }

        public int GetNextPathId()
        {
            return _priceDataAccess.Read().Count() + 1;
        }

        public IEnumerable<PriceDTO> Get()
        {
            return _priceDataAccess.Read().
                Select(x => new PriceDTO()
                {
                    Price_Id = x.PriceId,
                    PriceToConvert = x.PriceToConvert,
                    FromCurrency = x.FromCurrency,
                    ToCurrency = x.ToCurrency,
                    ExchangedValue = x.ExchangedValue,
                    DefaultValue = x.DefaultValue,
                    Shop = x.Shop,
                    Date = x.Date,
                    Product_Id = x.PRODUCT_ID,
                    PicturePathId = x.PicturePathId,
                    product = new ProductDTO() { Description = x.product.DESCRIPTION, Name = x.product.Name, Product_Id = x.PRODUCT_ID }
                });
        }

        public decimal GetExchangedValue(string priceToConvert, string to_currency)
        {
            var from_currency = "";
            decimal exchangedValue;
            Preprocess(priceToConvert);

            int totalIteration = 0;
            int final_index = -1;
            int collectionNumber = docCollection.DocumentList.Count - 1;

            List<DocumentVector> vSpace = VectorSpaceModel.ProcessDocumentCollection(docCollection);
            List<Centroid> resultSet = DocumnetClustering.PrepareDocumentCluster(collectionNumber, vSpace, ref totalIteration, ref final_index, currency);
            from_currency = resultSet[final_index].GroupedDocument[0].Content.Split(',')[0];
            WriteInCurrencyDocument(from_currency, currency);
            decimal rate = GetRate(from_currency, to_currency);
            exchangedValue = value * rate;
            return exchangedValue;
        }

        public string GetFromCurrency(string priceToConvert)
        {
            var from_currency = "";

            Preprocess(priceToConvert);

            int totalIteration = 0;
            int final_index = -1;
            int collectionNumber = docCollection.DocumentList.Count - 1;

            List<DocumentVector> vSpace = VectorSpaceModel.ProcessDocumentCollection(docCollection);
            List<Centroid> resultSet = DocumnetClustering.PrepareDocumentCluster(collectionNumber, vSpace, ref totalIteration, ref final_index, currency);
            from_currency = resultSet[final_index].GroupedDocument[0].Content.Split(',')[0];

            return from_currency;

        }

        private void InitializeDocuments()
        {
            string line;
            StreamReader file = new StreamReader(currency_path);
            while ((line = file.ReadLine()) != null)
            {
                docCollection.DocumentList.Add(line);
            }
            docCollection.DocumentList.Add(currency);
            file.Close();
        }

        private void WriteInCurrencyDocument(string from_currency, string pCurrency)
        {
            string[] fileContents = File.ReadAllLines(currency_path);

            for (int i = 0; i < fileContents.Length; ++i)
            {
                if (fileContents[i].StartsWith(from_currency))
                {
                    fileContents[i] += "," + pCurrency;
                }
            }
            
            File.WriteAllLines(currency_path, fileContents);
        }

        private void Preprocess(string price)
        {
            string newPrice = price;
            System.Text.StringBuilder numBuilder = new System.Text.StringBuilder("");
            newPrice = Regex.Replace(newPrice, @"\s+", "");
            newPrice = Regex.Replace(newPrice, "[,]", ".");
            Regex rg = new Regex(@"^[0-9.]*$");

            int count = 0;

            if (!rg.IsMatch(newPrice[0].ToString()))
            {
                while (!rg.IsMatch(newPrice[count].ToString()))
                    count++;
            }

            int index = count;

            while (rg.IsMatch(newPrice[count].ToString()))
            {
                numBuilder.Append(newPrice[count]);
                count++;
            }

            value = decimal.Parse(numBuilder.ToString());
            currency = (index == 0 ? newPrice.Substring(count, newPrice.Length - count) : newPrice.Substring(0, index));

            InitializeDocuments();
        }

        private decimal GetRate(string from_currency, string to_currency)
        {
            string url = "http://free.currencyconverterapi.com/api/v5/convert?q=" + from_currency + "_" + to_currency + "&compact=y";
            string result = "";
            string searched;

            using (var w = new WebClient())
            {
                result = w.DownloadString(url);
            }

            searched = result.Split(':')[2];
            decimal rate = decimal.Parse(searched.Substring(0, searched.Length - 2));
            return rate;
        }

    }
}
