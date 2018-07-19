using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmartPrice.Models;

namespace SmartPrice.Fragments
{
    public class ProductFragment : Fragment
    {
        private ListView lv;
        private ProductAdapter adapter;
        Context context;
        private JavaList<Product> products;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.ProductList, container, false);
            lv = root.FindViewById<ListView>(Resource.Id.productsList);
            context = root.Context;
            ProductsViewModel pvm = new ProductsViewModel();
            adapter = new ProductAdapter(context, pvm.Products);

            lv.Adapter = adapter;
            return root;
        }
    }
}