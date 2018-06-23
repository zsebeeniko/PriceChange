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
        byte[] picData;


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
                adapter = new ProductAdapter(context, GetProducts());

                lv.Adapter = adapter;
                lv.ItemClick += Lv_ItemClick;
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
                        var uri = "http://192.168.1.7/SmartPrice/api/Product/GetProducts";
                        var result = await client.GetStringAsync(uri);

                        //handling the answer  
                        var posts = JsonConvert.DeserializeObject<List<Post>>(result);

                        // generate the output  
                        var post = posts.First();
                        resultView.Text = "First post:\n\n" + post;
                    }
                };

                sendData.Click += async delegate
                {
                    using (var client = new HttpClient())
                    {
                        // Create a new post  
                        var novoPost = new Post
                        {
                            product_Id = 12,
                            shop = "My First Post",
                            description = "Macoratti .net - Quase tudo para .NET!",
                            Content = picData
                        };

                        // create the request content and define Json  
                        var json = JsonConvert.SerializeObject(novoPost);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        //  send a POST request  
                        var uri = "http://192.168.1.7/SmartPrice/api/Product/Submit";
                        var result = await client.PostAsync(uri, content);

                        // on error throw a exception  
                        result.EnsureSuccessStatusCode();

                        // handling the answer  
                        var resultString = await result.Content.ReadAsStringAsync();
                        var post = JsonConvert.DeserializeObject<Post>(resultString);

                        // display the output in TextView  
                        resultView.Text = post.ToString();
                    }
                };

            }

            ViewCompat.SetElevation(root, 50);
            return root;
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Android.Graphics.Bitmap bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(context);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.AdditionalProps, null);
            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(context);
            alertdialogbuilder.SetView(mView);


            MemoryStream memstream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Webp, 100, memstream);
            picData = memstream.ToArray();


            var shopField = mView.FindViewById<EditText>(Resource.Id.ShopTextField);
            var descriptionField = mView.FindViewById<EditText>(Resource.Id.DescriptionTextField);

            alertdialogbuilder.SetCancelable(false)
            .SetPositiveButton("Send", async delegate
            {
                using (var client = new HttpClient())
                {
                    // Create a new post  
                    var novoPost = new Post
                    {
                        product_Id = 12,
                        shop = "My First Post",
                        description = "Macoratti .net - Quase tudo para .NET!",
                        Content = picData
                    };

                    // create the request content and define Json  
                    var json = JsonConvert.SerializeObject(novoPost);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    //  send a POST request  
                    var uri = "http://192.168.1.7/SmartPrice/api/Product/Submit";
                    var result = await client.PostAsync(uri, content);

                    // on error throw a exception  
                    result.EnsureSuccessStatusCode();

                    // handling the answer  
                    var resultString = await result.Content.ReadAsStringAsync();
                    var post = JsonConvert.DeserializeObject<Post>(resultString);

                    // display the output in TextView  
                    resultView.Text = post.ToString();
                }
                Toast.MakeText(context, "Sent successfully! ", ToastLength.Short).Show();
            })
             .SetNegativeButton("Cancel", delegate
             {
                 alertdialogbuilder.Dispose();
             });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertdialogbuilder.Create();
            alertDialogAndroid.Show();
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(context, products[e.Position].Shop, ToastLength.Short).Show();
        }

        private JavaList<Product> GetProducts()
        {
            products = new JavaList<Product>();
            Product p;

            p = new Product("Picture 1", "Description1", Resource.Drawable.pic1);
            products.Add(p);

            p = new Product("Picture 2", "Description2", Resource.Drawable.pic2);
            products.Add(p);

            p = new Product("Picture 3", "Description3", Resource.Drawable.pic3);
            products.Add(p);

            p = new Product("Picture 4", "Description4", Resource.Drawable.pic4);
            products.Add(p);

            return products;
        }
    }
}