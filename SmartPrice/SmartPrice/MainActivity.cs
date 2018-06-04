using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Provider;
using Android.Runtime;
using Android.Graphics;
using Android.Support.V7.App;
using Android.Views;
using System.Net;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System.Drawing;
using RestSharp;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace SmartPrice
{
    [Activity(Label = "SmartPrice", MainLauncher = true, Theme="@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        ImageView imageView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var btnCamera = FindViewById<Button>(Resource.Id.btnCamera);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            btnCamera.Click += BtnCamera_Click;

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Android.Graphics.Bitmap bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.AdditionalProps, null);
            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertdialogbuilder.SetView(mView);


            MemoryStream memstream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Webp, 100, memstream);
            byte[] picData = memstream.ToArray();


            var shopField = mView.FindViewById<EditText>(Resource.Id.ShopTextField);
            var descriptionField = mView.FindViewById<EditText>(Resource.Id.DescriptionTextField);

            alertdialogbuilder.SetCancelable(false)
            .SetPositiveButton("Send", async delegate
             {
                 var product = new ProductDTO();

                 product.Shop = shopField.Text;
                 product.Description = descriptionField.Text;


                 WebClient client = new WebClient();
                 Uri uri = new Uri("http://localhost/SmartPrice/api/Products/Save");
                 NameValueCollection parameters = new NameValueCollection();
                 parameters.Add("Image", Convert.ToBase64String(picData));

                 client.UploadValuesAsync(uri, parameters);

                 //ProductDTO newProduct = await Submit(product);
                 Toast.MakeText(this, "Sent successfully! ", ToastLength.Short).Show();
             })
             .SetNegativeButton("Cancel", delegate
             {
                 alertdialogbuilder.Dispose();
             });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertdialogbuilder.Create();
            alertDialogAndroid.Show();
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }


        public async Task<ProductDTO> Submit(ProductDTO product)
        {
            var jsonObj = JsonConvert.SerializeObject(product);
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonObj.ToString(), Encoding.UTF8, "application/json");
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("http://localhost/SmartPrice/api/Product/Submit?shop=" + product.Shop + "&description=" + product.Description),
                    Method = HttpMethod.Post
                    //Content = content
                };
                var response = await client.SendAsync(request);
                string dataResult = response.Content.ReadAsStringAsync().Result;
                ProductDTO result = JsonConvert.DeserializeObject<ProductDTO>(dataResult);
                return result;
            }
        }

        private async void submit(ProductDTO product)
        {

            IRestClient client = new RestClient("http://localhost/SmartPrice/api/");
            IRestRequest request = new RestRequest("Product/Submit?product=" + product, Method.POST);
            try
            {
                await Task.Run(() =>
                    {
                        IRestResponse response = client.Execute(request);
                    }
                );
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
    }

    public class ConnectionResult<T>
    {
        public string Shop { get; set; }
        public string Description { get; set; }
        public T data
        {
            get; set;
        }
    }
}

