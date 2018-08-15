using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Gms.Vision;
using Android.Gms.Vision.Texts;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Widget;
using SmartPrice.Activities;
using SmartPrice.Models;
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

        private string convertString = "";
        private string to_currency = "";
        private string from_currency = "";
        private decimal value;

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

        public override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            RegisterService();
            registerMarker = true;

            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
            imageView.SetImageBitmap(bitmap);

            convertString = TextRecognition(bitmap);
            MemoryStream memstream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, memstream);
            byte[] picData = memstream.ToArray();

            using (var client = new HttpClient())
            {
                string currency_from = await client.GetStringAsync("http://192.168.0.103/SmartPrice/api/Product/GetFromCurrency?priceToConvert=" + convertString);
                from_currency = currency_from;

                var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);
                var localDataEdit = localDatas.Edit();
                string spinnerValue = localDatas.GetString("SpinnerValue", "");
                to_currency = spinnerValue.Split('-')[0];

                var exchanged = await client.GetStringAsync("http://192.168.0.103/SmartPrice/api/Product/GetExchangedValue?priceToConvert=" + convertString + "&to_currency=" + to_currency);
                exchanged = Regex.Replace(exchanged.ToString(), "[.]", ",");
                value = decimal.Parse(exchanged);

                string nextId = await client.GetStringAsync("http://192.168.0.103/SmartPrice/api/Picture/GetNextPathId");
                int picId = int.Parse(nextId);
                SavePicture(picData, picId);

                var smartPriceAct = new Intent(Application.Context, typeof(SubmitActivity));
                smartPriceAct.PutExtra("toCurr", to_currency);
                smartPriceAct.PutExtra("fromCurr", from_currency);
                smartPriceAct.PutExtra("exValue", value.ToString());
                smartPriceAct.PutExtra("picId", picId.ToString());
                smartPriceAct.PutExtra("priceToConvert", convertString);
                StartActivity(smartPriceAct);
            }
        }

        private string TextRecognition(Bitmap bitmap)
        {
            TextRecognizer textrec = new TextRecognizer.Builder(context).Build();
            StringBuilder builder = new StringBuilder();
            if (!textrec.IsOperational)
            {
                Log.Error("Error", "Detector dependencies are not yet available");
            }
            else
            {
                Frame frame = new Frame.Builder().SetBitmap(bitmap).Build();
                SparseArray items = textrec.Detect(frame);
                for (int i = 0; i < items.Size(); ++i)
                {
                    TextBlock text = (TextBlock)items.ValueAt(i);
                    builder.Append(text.Value);
                    builder.Append("\n");
                }
            }

            return builder.ToString();
        }

        public void SavePicture(byte[] picture, int picId)
        {
            var pictures = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string filePath = System.IO.Path.Combine(pictures, "pic" + picId + ".png");
            try
            {
                System.IO.File.WriteAllBytes(filePath, picture);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
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