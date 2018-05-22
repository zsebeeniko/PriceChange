using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;
using Android.Provider;
using Android.Runtime;
using Android.Graphics;

namespace SmartPrice
{
    [Activity(Label = "SmartPrice", MainLauncher = true)]
    public class MainActivity : Activity
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
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
        }
    }
}

