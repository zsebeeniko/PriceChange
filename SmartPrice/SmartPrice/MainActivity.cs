using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;
using com.refractored;
using Android.Support.V4.View;
using System;
using Android.Support.Design.Widget;
using Android.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using SmartPrice.Fragments;
using System.Drawing;
using Android.Gms.Maps;
using Com.Microsoft.Projectoxford.Vision;
using Android.Graphics;
using System.IO;
using Com.Microsoft.Projectoxford.Vision.Contract;
using GoogleGson;
using SmartPrice.Models;
using Newtonsoft.Json;
using Android.Gms.Vision.Texts;
using Android.Util;
using Android.Gms.Vision;

namespace SmartPrice
{
    [Activity( Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Android.Support.V4.Widget.DrawerLayout drawerLayout;
        private NavigationView navView;

        public VisionServiceRestClient client = new VisionServiceRestClient("7c8cc25104974c66916417134c548c90");
        private Bitmap bitmap;


        private ImageView imageview;
        private Button btnProcess;
        private TextView txtView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //// Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.Main);
            //imageview = FindViewById<ImageView>(Resource.Id.ocrimage);
            //btnProcess = FindViewById<Button>(Resource.Id.processbtn);
            //txtView = FindViewById<TextView>(Resource.Id.ocrtext);
            //Bitmap bitmap = BitmapFactory.DecodeResource(ApplicationContext.Resources, Resource.Drawable.abc);
            //imageview.SetImageBitmap(bitmap);
            //btnProcess.Click += delegate
            //{
            //    TextRecognizer txtRecognizer = new TextRecognizer.Builder(ApplicationContext).Build();
            //    if (!txtRecognizer.IsOperational)
            //    {
            //        Log.Error("Error", "Detector dependencies are not yet available");
            //    }
            //    else
            //    {
            //        Frame frame = new Frame.Builder().SetBitmap(bitmap).Build();
            //        SparseArray items = txtRecognizer.Detect(frame);
            //        StringBuilder strBuilder = new StringBuilder();
            //        for (int i = 0; i < items.Size(); i++)
            //        {
            //            TextBlock item = (TextBlock)items.ValueAt(i);
            //            strBuilder.Append(item.Value);
            //            strBuilder.Append("/");
            //        }
            //        txtView.Text = strBuilder.ToString();
            //    }
            //};



            var toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolBar.SetTitle(Resource.String.app_name);
            SetSupportActionBar(toolBar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.menu);

            drawerLayout = FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawer_layout);
            navView = FindViewById<NavigationView>(Resource.Id.nav_view);
            Android.App.FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            HomeFragment home = new HomeFragment();
            transaction.Add(Resource.Id.framelayout, home).Commit();


            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.car_array, Resource.Layout.SpinnerItem);
            adapter.SetDropDownViewResource(Resource.Layout.SpinnerDropdown);
            spinner.Adapter = adapter;

            setupDrawerContent(navView);

        }

        void setupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);
                Android.App.FragmentTransaction transaction1 = this.FragmentManager.BeginTransaction();
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.navmain:
                        HomeFragment home = new HomeFragment();
                        transaction1.Replace(Resource.Id.framelayout, home).AddToBackStack(null).Commit();
                        break;

                    case Resource.Id.navlist:
                        ProductFragment products = new ProductFragment();
                        transaction1.Replace(Resource.Id.framelayout, products).AddToBackStack(null).Commit();
                        break;
                    case Resource.Id.navmap:
                        MapsFragment maps = new MapsFragment();
                        transaction1.Replace(Resource.Id.framelayout, maps).AddToBackStack(null).Commit();
                        break;
                    case Resource.Id.navprice:
                        ExchangeRateFragment exchange = new ExchangeRateFragment();
                        transaction1.Replace(Resource.Id.framelayout, exchange).AddToBackStack(null).Commit();
                        break;
                    case Resource.Id.navcalendar:
                        CalendarFragment calendar = new CalendarFragment();
                        transaction1.Replace(Resource.Id.framelayout, calendar).AddToBackStack(null).Commit();
                        break;
                }
                drawerLayout.CloseDrawers();
            };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            string toast = string.Format("Selected car is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}

