using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SmartPrice.BL.BusinessLayerContracts.DTOs;

namespace SmartPrice
{
    class ProductsViewModel
    {
        private List<PriceDTO> products;

        public List<PriceDTO> Products => GetProducts();

        public List<PriceDTO> FilteredProducts(DateTime date) => GetFilteredProducts(date);

        private List<PriceDTO> GetProducts()
        {
            products = new List<PriceDTO>();

            using (var client = new HttpClient())
            {
                Task<string> result = client.GetStringAsync(Utils.baseUrl + "Price/GetPrices");
                products = JsonConvert.DeserializeObject<List<PriceDTO>>(result.Result);
            }

            return products;
        }

        private List<PriceDTO> GetFilteredProducts(DateTime date)
        {
            products = new List<PriceDTO>();

            using (var client = new HttpClient())
            {
                Task<string> result = client.GetStringAsync(Utils.baseUrl + "Price/GetFilteredPrices?date=" + date);
                products = JsonConvert.DeserializeObject<List<PriceDTO>>(result.Result);
            }

            return products;
        }
        
        public void DisplayAlert(PriceDTO item, Context context)
        {
            PriceDTO price = new PriceDTO();
            using (var client = new HttpClient())
            {
                Task<string> result = client.GetStringAsync(Utils.baseUrl + "Price/GetBestPrice?name=" + item.product.Name);
                price = JsonConvert.DeserializeObject<PriceDTO>(result.Result);
            }

            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(context);
            alertdialogbuilder.SetCancelable(false)
            .SetPositiveButton("OK", delegate
            {
                alertdialogbuilder.Dispose();
            });

            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertdialogbuilder.Create();
            alertDialogAndroid.SetTitle(price.ExchangedValue + " " + price.ToCurrency);
            alertDialogAndroid.SetMessage(price.Shop);
            alertDialogAndroid.Show();
        }
    }
}