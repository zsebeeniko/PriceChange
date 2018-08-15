using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SmartPrice.BL.BusinessLayerContracts.DTOs;

namespace SmartPrice.Activities
{
    [Activity(Label = "SubmitActivity", MainLauncher = false)]
    public class SubmitActivity : Activity
    {
        TextView toCurr;
        TextView fromCurr;
        TextView exchangedVal;
        int picId = 0;
        Spinner spin;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SubmitLayout);

            TextView toCurr = FindViewById<TextView>(Resource.Id.ToCurr);
            TextView fromCurr = FindViewById<TextView>(Resource.Id.FromCurr);
            TextView exchangedVal = FindViewById<TextView>(Resource.Id.ExValue);

            toCurr.Text = Intent.GetStringExtra("toCurr");
            fromCurr.Text = Intent.GetStringExtra("fromCurr");
            exchangedVal.Text = Intent.GetStringExtra("exValue");
            picId = int.Parse(Intent.GetStringExtra("picId"));

            ImageView arrow = FindViewById<ImageView>(Resource.Id.arrow);
            arrow.SetImageResource(Resource.Drawable.arrow);

            spin = FindViewById<Spinner>(Resource.Id.submitSpinner);
            spin.Visibility = ViewStates.Invisible;

            Button getNames = FindViewById<Button>(Resource.Id.getNames);
            getNames.Click += GetNames_Click;

            Button submit = FindViewById<Button>(Resource.Id.Submit);
            submit.Click += Submit_Click;
        }

        private async void Submit_Click(object sender, EventArgs e)
        {
            ProductDTO product = new ProductDTO();

            EditText Name = FindViewById<EditText>(Resource.Id.productName);
            EditText Description = FindViewById<EditText>(Resource.Id.productDescription);

            product.Product_Id = -1;
            product.Name = Name.Text;
            product.Description = Description.Text;

            PriceDTO price = new PriceDTO();

            EditText Shop = FindViewById<EditText>(Resource.Id.ShopTextField);

            price.Price_Id = -1;
            price.Shop = Shop.Text;
            price.ToCurrency = toCurr.Text;
            price.FromCurrency = fromCurr.Text;
            price.ExchangedValue = decimal.Parse(exchangedVal.Text);
            price.PicturePathId = picId;
            price.PriceToConvert = Intent.GetStringExtra("priceToConvert");

            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(product);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(json, Encoding.UTF8, "application/json"));
                    var uri = "http://192.168.0.103/SmartPrice/api/Product/Submit";
                    var result = await client.PostAsync(uri, content);
                    int product_id = int.Parse(result.ToString());

                    price.Product_Id = product_id;
                    json = JsonConvert.SerializeObject(price);
                    content = new MultipartFormDataContent();
                    content.Add(new StringContent(json, Encoding.UTF8, "application/json"));
                    uri = "http://192.168.0.103/SmartPrice/api/Price/Submit";
                    result = await client.PostAsync(uri, content);

                    Toast.MakeText(this, "Sent successfully! ", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }
        private async void GetNames_Click(object sender, EventArgs e)
        {
            spin.Visibility = ViewStates.Visible;
            using (var client = new HttpClient())
            {
                var currency_from = await client.GetAsync("http://192.168.0.103/SmartPrice/api/Product/GetProductNames");
            }
        }
    }
}