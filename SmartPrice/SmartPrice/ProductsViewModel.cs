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

namespace SmartPrice
{
    class ProductsViewModel
    {
        private JavaList<Product> products;

        public JavaList<Product> Products => GetProducts();

        private JavaList<Product> GetProducts()
        {
                products = new JavaList<Product>();
            Product p;

            //p = new Product("Picture 1", "Description1", Resource.Drawable.pic1);
            //products.Add(p);

            //p = new Product("Picture 2", "Description2", Resource.Drawable.pic2);
            //products.Add(p);

            //p = new Product("Picture 3", "Description3", Resource.Drawable.pic3);
            //products.Add(p);

            //p = new Product("Picture 4", "Description4", Resource.Drawable.pic4);
            //products.Add(p);

            return products;
        }
    }
}