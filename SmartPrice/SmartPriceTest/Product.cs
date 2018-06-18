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

namespace SmartPriceTest
{
    class Product
    {
        private int image;
        private String name;

        public Product(String name, int image)
        {
            this.name = name;
            this.image = image;
        }

        public String Name
        {
            get { return name; }
        }

        public int Image
        {
            get { return image; }
        }
    }
}