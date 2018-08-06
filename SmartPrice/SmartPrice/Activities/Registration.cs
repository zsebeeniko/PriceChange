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
    [Activity(Label = "Registration")]
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

            regButton.Click += RegButton_Click; 
            // Create your application here
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