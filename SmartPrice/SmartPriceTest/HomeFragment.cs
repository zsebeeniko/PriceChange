using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartPriceTest
{
    public class HomeFragment : Fragment
    {
        ImageView imageView;
        Context context;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CameraFragment, container, false);
            context = view.Context;
            imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
            return view;
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);

            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(context);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.AdditionalProps, null);
            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(context);
            alertdialogbuilder.SetView(mView);

            MemoryStream memstream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, memstream);
            byte[] picData = memstream.ToArray();

            var shopField = mView.FindViewById<EditText>(Resource.Id.ShopTextField);
            var descriptionField = mView.FindViewById<EditText>(Resource.Id.DescriptionTextField);

            alertdialogbuilder.SetCancelable(false)
            .SetPositiveButton("Send", async delegate
            {
                // using (var client = new HttpClient())
                // {
                //     try
                //     {
                //         // Create a new post  
                //         var product = new Product()
                //         {
                //             Product_Id = 1,
                //             Shop = shopField.Text,
                //             Description = descriptionField.Text,
                //             Picture = picData
                //         };

                //         // create the request content and define Json  
                //         var json = JsonConvert.SerializeObject(product);
                //         var content = new MultipartFormDataContent();
                //         content.Add(new StringContent(json, Encoding.UTF8, "application/json"));
                //         //  send a POST request  
                //         var uri = "http://192.168.0.110/SmartPrice/api/Product/Submit";
                //         var result = await client.PostAsync(uri, content);
                //         //var result = await client.GetAsync(uri);
                //         result.EnsureSuccessStatusCode();

                Toast.MakeText(context, "Sent successfully! ", ToastLength.Short).Show();
                //     }
                //     catch
                //(Exception e)
                //     {
                //         Console.Write(e.ToString());
                //     }
                // }
            })
             .SetNegativeButton("Cancel", delegate
             {
                 alertdialogbuilder.Dispose();
             });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertdialogbuilder.Create();
            alertDialogAndroid.Show();
        }

    }
}