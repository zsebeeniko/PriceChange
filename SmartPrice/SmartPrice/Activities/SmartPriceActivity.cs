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
        private TextView firstName;
        private TextView lastName;
        private ImageView userImage;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            var toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolBar.SetTitle(Resource.String.app_name);
            SetSupportActionBar(toolBar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.menu2);

            var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);

            drawerLayout = FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawer_layout);
            navView = FindViewById<NavigationView>(Resource.Id.nav_view);
            Android.App.FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            HomeFragment home = new HomeFragment();
            transaction.Add(Resource.Id.framelayout, home).Commit();

            var headerView = navView.GetHeaderView(0);
            lastName = headerView.FindViewById<TextView>(Resource.Id.menuLastName);
            firstName = headerView.FindViewById<TextView>(Resource.Id.menuFirstName);
            userImage = headerView.FindViewById<ImageView>(Resource.Id.userImg);

            string data_lastName= localDatas.GetString("LastName", "");
            string data_firstName = localDatas.GetString("FirstName", "");
            string data_uri = localDatas.GetString("Uri", "");
            lastName.Text = data_lastName;
            firstName.Text = data_firstName;

            Android.Net.Uri uri = Android.Net.Uri.Parse(data_uri);
            if (uri == null)
            {
                userImage.SetImageResource(Resource.Drawable.user);
            }
            else
            {
                userImage.SetImageURI(uri);
            }

            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);
            spinner.ItemSelected += spinner_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.curr_array, Resource.Layout.SpinnerItem);
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
            int position = localDatas.GetInt("Position", -1);
            Boolean fromReg = localDatas.GetBoolean("FromReg", false);
            if (position > -1 && fromReg)
            {
                spinner.SetSelection(position);
                localDataEdit.PutBoolean("FromReg", false);
            }
            else
            {
                string spinnerValue = spinner.GetItemAtPosition(e.Position).ToString();
                localDataEdit.PutString("SpinnerValue", spinnerValue);
                localDataEdit.PutInt("Position", e.Position);
                spinner.SetSelection(e.Position);
                localDataEdit.PutBoolean("FromReg", true);
            }
            localDataEdit.Commit();
            string toast = string.Format("Selected value is {0}", spinner.SelectedItem.ToString());
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}

