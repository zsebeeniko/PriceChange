using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace SmartPrice.Activities
{
    [Activity(Label = "SmartPrice", MainLauncher = false)]
    public class Registration : Activity
    {
        string first_name = string.Empty;
        string last_name = string.Empty;

        Android.Net.Uri uri;

        public static readonly int PickImageId = 1000;
        ImageView userImage;

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                uri = data.Data;
                userImage.SetImageURI(uri);
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registration);

            userImage = FindViewById<ImageView>(Resource.Id.regUserImg);
            var regButton = FindViewById<Button>(Resource.Id.registration);
            
            userImage.SetImageResource(Resource.Drawable.user);

            Spinner spinner = FindViewById<Spinner>(Resource.Id.regSpinner);
            spinner.ItemSelected += spinner_ItemSelected;
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.curr_array, Resource.Layout.RegSpinnerItem);
            adapter.SetDropDownViewResource(Resource.Layout.SpinnerDropdown);
            spinner.Adapter = adapter;

            Button button = FindViewById<Button>(Resource.Id.loadPic);
            button.Click += ButtonOnClick;

            regButton.Click += RegButton_Click; 
        }

        void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            Intent = new Intent();
            Intent.SetType("image/*");
            Intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
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

            string toast = string.Format("Selected value is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            var localDatas = Application.Context.GetSharedPreferences("MyDatas", Android.Content.FileCreationMode.Private);
            var localDataEdit = localDatas.Edit();

            var firstName = FindViewById<EditText>(Resource.Id.firstname);
            var lastName = FindViewById<EditText>(Resource.Id.lastname);

            first_name = firstName.Text;
            last_name = lastName.Text;

            localDataEdit.PutString("FirstName", first_name);
            localDataEdit.PutString("LastName", last_name);
            localDataEdit.PutString("Name", first_name + " " + last_name);
            localDataEdit.PutString("Uri", uri.ToString());
            localDataEdit.Commit();

            var smartPriceAct = new Intent(Application.Context, typeof(SmartPriceActivity));
            StartActivity(smartPriceAct);
        }
    }
}