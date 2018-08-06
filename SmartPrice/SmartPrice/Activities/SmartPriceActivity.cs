using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Support.Design.Widget;
using Android.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using SmartPrice.Fragments;

namespace SmartPrice.Activities
{
    [Activity(Label = "SmartPriceActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = false)]
    public class SmartPriceActivity : AppCompatActivity
    {
        private Android.Support.V4.Widget.DrawerLayout drawerLayout;
        private NavigationView navView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

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
            var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);
            int position = localDatas.GetInt("Position", 0);
            spinner.SetSelection(position);
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

            var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);
            var localDataEdit = localDatas.Edit();
            string spinnerValue = spinner.GetItemAtPosition(e.Position).ToString();
            localDataEdit.PutString("SpinnerValue", spinnerValue);
            localDataEdit.PutInt("Position", e.Position);
            localDataEdit.Commit();

            string toast = string.Format("Selected car is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}

