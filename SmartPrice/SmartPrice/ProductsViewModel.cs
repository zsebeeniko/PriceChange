using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
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
            var pictures = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = System.IO.Path.Combine(pictures, "pic1.png");
            var images = LoadImages(filePath);
            Product p;

            p = new Product("Picture 1", "Description1", images[0]);
            products.Add(p);

            return products;
        }

        private List<Bitmap> LoadImages(String path)
        {
            List<Bitmap> pics = new List<Bitmap>();
            try
            {
                var f = File.ReadAllBytes(path);
                Bitmap b = BitmapFactory.DecodeStream(new MemoryStream(f));
                pics.Add(b);
            }
            catch (FileNotFoundException e)
            {
                Console.Write(e.Message);
            }

            return pics;
        }
    }
}