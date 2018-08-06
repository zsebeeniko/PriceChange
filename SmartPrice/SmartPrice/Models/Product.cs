using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartPrice.Models
{
    class Product
    {
        //private int Product_Id;
        //private String Shop;
        //private String Description;
        //private byte[] Picture;


        public int Product_Id { get; set; }
        public string Shop { get; set; }
        public string Description { get; set; }
        public Bitmap Picture { get; set; }

        public override string ToString()
        {
            return string.Format(
                "Post Id: {0}\nShop: {1}\nDescription: {1}\nPicture: {2}",
                Product_Id, Shop, Description, Picture);
        }

        public Product( String shop, String description, Bitmap picture)
        {
            this.Shop = shop;
            this.Description = description;
            this.Picture = picture;
        }

        //public int product_id
        //{
        //    get { return Product_Id; }
        //}

        //public String shop
        //{
        //    get { return Shop; }
        //}

        //public String description
        //{
        //    get { return Description; }
        //}


        //public byte[] picture
        //{
        //    get { return Picture; }
        //}
    }
}