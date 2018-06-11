using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
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
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using SmartPrice.Models;

namespace SmartPrice
{
    public class ContentFragment : Fragment
    {
        private int position;
        ImageView imageView;
        Context context;
        private ListView listView;
        private ProductAdapter adapter;
        private JavaList<Product> products;

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
                var text = root.FindViewById<TextView>(Resource.Id.textView);
                text.Text = "Camera Page";
                context = root.Context;
                imageView = root.FindViewById<ImageView>(Resource.Id.imageView);
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            }
            else
            {
                root = inflater.Inflate(Resource.Layout.ProductList, container, false);
                var text = root.FindViewById<TextView>(Resource.Id.textView);
                text.Text = "List of Products";
                listView = root.FindViewById<ListView>(Resource.Id.productsListView);
                context = root.Context;
                adapter = new ProductAdapter(context, GetProducts());

                listView.Adapter = adapter;

                listView.ItemClick += listView_ItemClick;
            }
            ViewCompat.SetElevation(root, 50);
            return root;
        }

        private JavaList<Product> GetProducts()
        {
            products = new JavaList<Product>();

            Product p;

            p = new Product("Picture1", "Description1", Resource.Drawable.pic1);
            products.Add(p);

            p = new Product("Picture2", "Description2", Resource.Drawable.pic2);
            products.Add(p);

            p = new Product("Picture3", "Description3",  Resource.Drawable.pic3);
            products.Add(p);

            p = new Product("Picture4", "Description4", Resource.Drawable.pic4);
            products.Add(p);

            return products;

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
                Toast.MakeText(context, "Sent successfully! ", ToastLength.Short).Show();
            })
             .SetNegativeButton("Cancel", delegate
             {
                 alertdialogbuilder.Dispose();
             });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertdialogbuilder.Create();
            alertDialogAndroid.Show();
        }

        void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(context, products[e.Position].Shop, ToastLength.Short).Show();
        }
    }
}