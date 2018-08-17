using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using SmartPrice.Models;

namespace SmartPrice.Fragments
{
    public class ProductFragment : Fragment
    {
        private ListView lv;
        private ProductAdapter adapter;
        Context context;
        ProductsViewModel pvm;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.ProductList, container, false);
            lv = root.FindViewById<ListView>(Resource.Id.productsList);
            context = root.Context;
            
            pvm = new ProductsViewModel();
            adapter = new ProductAdapter(context, pvm.Products);

            lv.Adapter = adapter;
            lv.ItemClick += Lv_ItemClick;
            return root;
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            PriceDTO item = adapter.GetItemById(e.Position);
            pvm.DisplayAlert(item, context);
        }
    }
}