using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;
using com.refractored;
using Android.Support.V4.View;
using Java.IO;
using Android.Graphics;
using Android.Content;
using Android.Provider;
using Android.Content.PM;
using System.Collections.Generic;
using System;
using Android.Support.Design.Widget;
using Android.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace SmartPriceTest
{

    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Android.Support.V4.Widget.DrawerLayout drawerLayout;
        private NavigationView navView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var toolBar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolBar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.menu4);
            drawerLayout = FindViewById<Android.Support.V4.Widget.DrawerLayout>(Resource.Id.drawer_layout);
            navView = FindViewById<NavigationView>(Resource.Id.nav_view);
            Android.App.FragmentTransaction transaction = this.FragmentManager.BeginTransaction();
            HomeFragment home = new HomeFragment();
            transaction.Add(Resource.Id.framelayout, home).Commit();

            setupDrawerContent(navView);
        }

        void setupDrawerContent(NavigationView navigationView)
        {
            navigationView.NavigationItemSelected += (sender, e) => {
                e.MenuItem.SetChecked(true);
                Android.App.FragmentTransaction transaction1 = this.FragmentManager.BeginTransaction();
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_main:
                        HomeFragment home = new HomeFragment();
                        transaction1.Replace(Resource.Id.framelayout, home).AddToBackStack(null).Commit();
                        break;

                    //case Android.Resource.Id.nav_list:
                    //    VideoFragment video = new VideoFragment();
                    //    transaction1.Replace(Resource.Id.framelayout, video).AddToBackStack(null).Commit();
                    //    break;
                }
                drawerLayout.CloseDrawers();
            };
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}

