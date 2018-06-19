using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace SmartPriceTest
{
    public class ContentFragment : Fragment
    {
        private int position;
        private ListView lv;
        private ProductAdapter adapter;
        private JavaList<Product> products;
        ImageView imageView;
        Context context;

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
            TextView text;

            if (position == 0)
            {
                root = inflater.Inflate(Resource.Layout.CameraFragment, container, false);
                text = root.FindViewById<TextView>(Resource.Id.textView);
                text.Text = "Camera Page";
                context = root.Context;
                imageView = root.FindViewById<ImageView>(Resource.Id.imageView);
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            }
            else
            {
                root = inflater.Inflate(Resource.Layout.ProductList, container, false);
                text = root.FindViewById<TextView>(Resource.Id.textView);
                text.Text = "Product Lists";
                lv = root.FindViewById<ListView>(Resource.Id.productsList);
                context = root.Context;
                adapter = new ProductAdapter(context, GetProducts());

                lv.Adapter = adapter;
                lv.ItemClick += Lv_ItemClick;
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
            byte[] picData = memstream.ToArray();


            var shopField = mView.FindViewById<EditText>(Resource.Id.ShopTextField);
            var descriptionField = mView.FindViewById<EditText>(Resource.Id.DescriptionTextField);

            alertdialogbuilder.SetCancelable(false)
            .SetPositiveButton("Send", delegate
            {
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
            Toast.MakeText(context, products[e.Position].Name, ToastLength.Short).Show();
        }

        private JavaList<Product> GetProducts()
        {
            products = new JavaList<Product>();
            Product p;

            p = new Product("Picture 1", Resource.Drawable.pic1);
            products.Add(p);

            p = new Product("Picture 2", Resource.Drawable.pic2);
            products.Add(p);

            p = new Product("Picture 3", Resource.Drawable.pic3);
            products.Add(p);

            p = new Product("Picture 4", Resource.Drawable.pic4);
            products.Add(p);

            return products;
        }
    }
}