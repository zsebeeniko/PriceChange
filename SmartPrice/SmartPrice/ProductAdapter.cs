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
using Object = Java.Lang.Object;

namespace SmartPrice
{
    class ProductAdapter : BaseAdapter
    {
        private readonly Context c;
        private readonly JavaList<Product> products;
        private LayoutInflater inflater;

        /*
         * CONSTRUCTOR
         */
        public ProductAdapter(Context c, JavaList<Product> products)
        {
            this.c = c;
            this.products = products;
        }

        /*
         * RETURN SPACECRAFT
         */
        public override Object GetItem(int position)
        {
            return products.Get(position);
        }

        /*
         * SPACECRAFT ID
         */
        public override long GetItemId(int position)
        {
            return position;
        }

        /*
         * RETURN INFLATED VIEW
         */
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //INITIALIZE INFLATER
            if (inflater == null)
            {
                inflater = (LayoutInflater)c.GetSystemService(Context.LayoutInflaterService);
            }

            //INFLATE OUR MODEL LAYOUT
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.ProductRow, parent, false);
            }

            //BIND DATA
            ProductAdapterViewHolder holder = new ProductAdapterViewHolder(convertView)
            {
                Shop = { Text = products[position].Shop },
                Description = {Text = products[position].Description}
            };
            holder.Img.SetImageResource(products[position].Picture);

            //convertView.SetBackgroundColor(Color.LightBlue);

            return convertView;
        }

        /*
         * TOTAL NUM OF SPACECRAFTS
         */
        public override int Count
        {
            get { return products.Size(); }
        }
    }

    class ProductAdapterViewHolder : Java.Lang.Object
    {
        //adapter views to re-use
        public TextView Shop;
        public TextView Description;
        public ImageView Img;

        public ProductAdapterViewHolder(View itemView)
        {
            Shop = itemView.FindViewById<TextView>(Resource.Id.productShop);
            Description = itemView.FindViewById<TextView>(Resource.Id.productDescription);
            Img = itemView.FindViewById<ImageView>(Resource.Id.productImage);

        }
    }
}