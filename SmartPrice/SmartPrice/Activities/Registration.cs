using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartPrice.Activities
{
    [Activity(Label = "SmartPrice", MainLauncher = true)]
    public class Registration : Activity
    {
        string name = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registration);

            var firstName = FindViewById<EditText>(Resource.Id.firstname);
            var lastName = FindViewById<EditText>(Resource.Id.lastname);
            var regButton = FindViewById<Button>(Resource.Id.registration);

            string first_name = firstName.Text;
            string last_name = lastName.Text;
            name = first_name + " " + last_name;

            Spinner spinner = FindViewById<Spinner>(Resource.Id.regSpinner);
            spinner.ItemSelected += spinner_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.car_array, Resource.Layout.RegSpinnerItem);
            adapter.SetDropDownViewResource(Resource.Layout.SpinnerDropdown);
            spinner.Adapter = adapter;

            regButton.Click += RegButton_Click; 
            // Create your application here
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);
            var localDataEdit = localDatas.Edit();
            string spinnerValue = spinner.GetItemAtPosition(e.Position).ToString();
            localDataEdit.PutString("SpinnerValue", spinnerValue);
            localDataEdit.PutInt("Position", e.Position);
            localDataEdit.PutBoolean("FromReg", true);
            localDataEdit.Commit();

            string toast = string.Format("Selected car is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);
            var localDataEdit = localDatas.Edit();
            localDataEdit.PutString("Name", name);
            localDataEdit.Commit();

            var smartPriceAct = new Intent(Application.Context, typeof(SmartPriceActivity));
            StartActivity(smartPriceAct);
        }
    }
}