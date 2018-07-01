using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using SmartPrice.Models;

namespace SmartPrice
{
    public class ContentFragment : Fragment
    {
        private int position;
        private ListView lv;
        private ProductAdapter adapter;
        private JavaList<Product> products;
        ImageView imageView;
        Context context;
        Button readJson;
        Button sendData;
        TextView resultView;

        public static ContentFragment NewInstance(int position)
        {
            var f = new ContentFragment();
            var b = new Bundle();
            b.PutInt("position", position);
            f.Arguments = b;
            return f;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root;

            if (position == 0)
            {
                root = inflater.Inflate(Resource.Layout.CameraFragment, container, false);
                context = root.Context;
                imageView = root.FindViewById<ImageView>(Resource.Id.imageView);
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            }
            else if (position == 1)
            {
                root = inflater.Inflate(Resource.Layout.ProductList, container, false);
                lv = root.FindViewById<ListView>(Resource.Id.productsList);
                context = root.Context;
                ProductsViewModel pvm = new ProductsViewModel();
                adapter = new ProductAdapter(context, pvm.Products);

                lv.Adapter = adapter;
            }
            else
            {
                root = inflater.Inflate(Resource.Layout.RestTest, container, false);
                readJson = root.FindViewById<Button>(Resource.Id.readJson);
                sendData = root.FindViewById<Button>(Resource.Id.sendData);
                resultView = root.FindViewById<TextView>(Resource.Id.resultView);

                readJson.Click += async delegate
                {
                    using (var client = new HttpClient())
                    {
                        // send a GET request  
                        var uri = "http://192.168.0.110/SmartPrice/api/Product/GetProducts";
                        //var result = await client.GetAsync(uri);

                        var response = await client.GetAsync(uri).ConfigureAwait(false);
                        response.EnsureSuccessStatusCode();
                        var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var products = JsonConvert.DeserializeObject<List<Product>>(responseContent);


                        //handling the answer  
                        //var posts = JsonConvert.DeserializeObject<List<Product>>(result.ToString());

                        ////// generate the output  
                        //var post = products.First();
                        //resultView.Text = "First post:\n\n" + post.ToString();
                    }
                };
            }

            ViewCompat.SetElevation(root, 50);
            return root;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);

            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(context);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.AdditionalProps, null);
            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(context);
            alertdialogbuilder.SetView(mView);

            MemoryStream memstream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, memstream);
            byte[] picData = memstream.ToArray();

            var shopField = mView.FindViewById<EditText>(Resource.Id.ShopTextField);
            var descriptionField = mView.FindViewById<EditText>(Resource.Id.DescriptionTextField);

            alertdialogbuilder.SetCancelable(false)
            .SetPositiveButton("Send", async delegate
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                    // Create a new post  
                    var product = new Product()
                    {
                        Product_Id = 1,
                        Shop = shopField.Text,
                        Description = descriptionField.Text,
                        Picture = picData
                    };

                    // create the request content and define Json  
                    var json = JsonConvert.SerializeObject(product);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(json, Encoding.UTF8, "application/json"));
                    //  send a POST request  
                    var uri = "http://192.168.0.110/SmartPrice/api/Product/Submit";
                    var result = await client.PostAsync(uri, content);
                        //var result = await client.GetAsync(uri);
                    result.EnsureSuccessStatusCode(); 
                        
                    Toast.MakeText(context, "Sent successfully! ", ToastLength.Short).Show();
                    }catch
                (Exception e)
                    {
                        Console.Write(e.ToString());
                    }
                }
            })
             .SetNegativeButton("Cancel", delegate
             {
                 alertdialogbuilder.Dispose();
             });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertdialogbuilder.Create();
            alertDialogAndroid.Show();
        }
    }
}