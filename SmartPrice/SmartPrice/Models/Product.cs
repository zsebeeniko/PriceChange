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

namespace SmartPrice.Models
{
    class Product
    {
        private int image;
        private String shop;
        private String description;

        public Product(String shop, String description, int image)
        {
            this.shop = shop;
            this.description = description;
            this.image = image;
        }

        public String Shop
        {
            get { return shop; }
        }

        public String Description
        {
            get { return description; }
        }

        public int Image
        {
            get { return image; }
        }
    }
}