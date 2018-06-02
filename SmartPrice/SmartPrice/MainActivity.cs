using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Provider;
using Android.Runtime;
using Android.Graphics;
using Android.Support.V7.App;
using Android.Views;
using System.Net;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System.Drawing;

namespace SmartPrice
{
    [Activity(Label = "SmartPrice", MainLauncher = true, Theme="@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        ImageView imageView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var btnCamera = FindViewById<Button>(Resource.Id.btnCamera);
            imageView = FindViewById<ImageView>(Resource.Id.imageView);

            btnCamera.Click += BtnCamera_Click;

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Android.Graphics.Bitmap bitmap = (Android.Graphics.Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);
            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(this);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.AdditionalProps, null);
            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertdialogbuilder.SetView(mView);

            var shopField = mView.FindViewById<EditText>(Resource.Id.ShopTextField);
            var descriptionField = mView.FindViewById<EditText>(Resource.Id.DescriptionTextField);

            alertdialogbuilder.SetCancelable(false)
            .SetPositiveButton("Send", delegate
             {
                 var product = new ProductDTO();
                 ImageConverter converter = new ImageConverter();
                 product.Picture = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
                 product.Shop = shopField.Text;
                 product.Description = descriptionField.Text;
                 var request = WebRequest.Create(string.Format(@"http://SmartPrice/api/Product/Submit?product=" + product));
                 Toast.MakeText(this, "Sent successfully! ", ToastLength.Short).Show();
             })
             .SetNegativeButton("Cancel", delegate
             {
                 alertdialogbuilder.Dispose();
             });
            Android.Support.V7.App.AlertDialog alertDialogAndroid = alertdialogbuilder.Create();
            alertDialogAndroid.Show();
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
    }
}

