using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Graphics;
using Android.Locations;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SmartPrice.VieModels;

namespace SmartPrice.Fragments
{
    public class HomeFragment : Fragment
    {
        ImageView imageView;
        Context context;
        MarkersViewModel markersViewModel;
        List<MarkerOptions> markers;
        Boolean registerMarker = false;

        GPSServiceBinder _binder;
        GPSServiceConnection _gpsServiceConnection;
        Intent _gpsServiceIntent;
        private GPSServiceReciever _receiver;

        public static HomeFragment instance;
        public override void OnCreate(Bundle savedInstanceState)
        {
            instance = this;
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CameraFragment, container, false);
            context = view.Context;
            instance.markersViewModel = new MarkersViewModel();
            imageView = view.FindViewById<ImageView>(Resource.Id.imageView);
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(intent, 0);
            return view;
        }

        public override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            RegisterService();
            registerMarker = true;

            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);

            LayoutInflater layoutInflaterAndroid = LayoutInflater.From(context);
            View mView = layoutInflaterAndroid.Inflate(Resource.Layout.AdditionalProps, null);
            Android.Support.V7.App.AlertDialog.Builder alertdialogbuilder = new Android.Support.V7.App.AlertDialog.Builder(context);
            alertdialogbuilder.SetView(mView);


            TextRecognizer textrec = new TextRecognizer.Builder(context).Build();
            if(!textrec.IsOperational)
            {
                Log.Error("Error", "Detector dependencies are not yet available");
            }
            else
            {
                Frame frame = new Frame.Builder().SetBitmap(bitmap).Build();
                SparseArray items = textrec.Detect(frame);
                StringBuilder builder = new StringBuilder();
                for(int i=0; i<items.Size(); ++i)
                {
                    TextBlock text = (TextBlock)items.ValueAt(i);
                    builder.Append(text.Value);
                    builder.Append("\n");
                }

                var textField = mView.FindViewById<TextView>(Resource.Id.AdditionalProp);
                textField.Text = builder.ToString(); 

            }

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

        private void RegisterService()
        {
            _gpsServiceConnection = new GPSServiceConnection(_binder);
            _gpsServiceIntent = new Intent(Android.App.Application.Context, typeof(GPSService));
            Activity.BindService(_gpsServiceIntent, _gpsServiceConnection, Bind.AutoCreate);
        }
        private void RegisterBroadcastReceiver()
        {
            IntentFilter filter = new IntentFilter(GPSServiceReciever.LOCATION_UPDATED);
            filter.AddCategory(Intent.CategoryDefault);
            _receiver = new GPSServiceReciever();
            Activity.RegisterReceiver(_receiver, filter);
        }

        private void UnRegisterBroadcastReceiver()
        {
            Activity.UnregisterReceiver(_receiver);
        }
        public void UpdateUI(Intent intent)
        {
            string Title = intent.GetStringExtra("Address");
            double lat = intent.GetDoubleExtra("Latitude", 0.0);
            double lng = intent.GetDoubleExtra("Longitude", 0.0);
            Models.Marker marker = new Models.Marker(Title, lat, lng);
            markers = markersViewModel.AddMarker(marker);
            Console.Write(markers.Count);
        }

        public MarkersViewModel MarkersViewModelInstance()
        {
            return markersViewModel;
        }

        public override void OnResume()
        {
            base.OnResume();
            RegisterBroadcastReceiver();
        }

        public override void OnPause()
        {
            base.OnPause();
            UnRegisterBroadcastReceiver();
        }

        [BroadcastReceiver]
        internal class GPSServiceReciever : BroadcastReceiver
        {
            public static readonly string LOCATION_UPDATED = "LOCATION_UPDATED";
            public override void OnReceive(Context context, Intent intent)
            {
                if (intent.Action.Equals(LOCATION_UPDATED) && instance.registerMarker == true)
                {
                    instance.UpdateUI(intent);
                    instance.registerMarker = false;
                }

            }
        }
    }
}