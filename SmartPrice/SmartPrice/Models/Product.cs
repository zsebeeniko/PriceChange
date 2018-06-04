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
        private String shop;
        private String description;
        private int picture;

        public Product(string shop, string desc, int image)
        {
            this.shop = shop;
            this.description = desc;
            this.picture = image;
        }

        public string Shop
        {
            get { return shop; }
        }

        public string Description
        {
            get { return description; }
        }

        public int Picture
        {
            get { return picture; }
        }
    }

}