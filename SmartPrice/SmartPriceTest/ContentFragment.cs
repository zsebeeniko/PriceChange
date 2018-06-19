using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
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