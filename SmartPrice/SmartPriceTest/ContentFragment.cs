using System;
using System.Collections.Generic;
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
using Android.Net;
using Java.IO;

using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using Android.Graphics.Drawables;

namespace SmartPriceTest
{
    public class ContentFragment : Fragment
    {
        private int position;
        private ListView lv;
        private ProductAdapter adapter;
        private JavaList<Product> products;

        public ImageView imageView;
        Context context;
        Android.Net.Uri selectedImage;

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
                context = root.Context;
                imageView = root.FindViewById<ImageView>(Resource.Id.imageView);
                Intent intent = new Intent(MediaStore.ActionImageCapture);  
                StartActivityForResult(intent, 0);
            }
            else
            {
                root = inflater.Inflate(Resource.Layout.ProductList, container, false);
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

            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(context);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.AdditionalProps, null);
            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(context);
            alertdialogbuilder.SetView(mView);

            System.IO.MemoryStream memstream = new System.IO.MemoryStream();
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
            Toast.MakeText(context, products[e.Position].Shop, ToastLength.Short).Show();
        }       
    }
}