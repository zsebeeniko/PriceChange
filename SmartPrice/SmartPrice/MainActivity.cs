using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Support.Design.Widget;
using Android.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using SmartPrice.Fragments;
using Android.Content;
using SmartPrice.Activities;

namespace SmartPrice
{
    [Activity(Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = false)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
           
            var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);
            string name = localDatas.GetString("Name", string.Empty);
            if (name == string.Empty)
            {
                var registrationAct = new Intent(this, typeof(Registration));
                StartActivity(registrationAct);
            }
            else
            {
                var smartPriceAct = new Intent(this, typeof(SmartPriceActivity));
                StartActivity(smartPriceAct);
            }

        }
    }
}

