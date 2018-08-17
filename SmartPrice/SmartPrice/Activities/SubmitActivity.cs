using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using SmartPrice.BL.BusinessLayerContracts.DTOs;

namespace SmartPrice.Activities
{
    [Activity(Label = "SubmitActivity", MainLauncher = false)]
    public class SubmitActivity : Activity
    {
        TextView toCurr;
        TextView fromCurr;
        TextView exchangedVal;
        int picId = 0;
        Spinner spin;
        string spinnerValue = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SubmitLayout);

            toCurr = FindViewById<TextView>(Resource.Id.ToCurr);
            fromCurr = FindViewById<TextView>(Resource.Id.FromCurr);
            exchangedVal = FindViewById<TextView>(Resource.Id.ExValue);

            toCurr.Text = Intent.GetStringExtra("toCurr");
            fromCurr.Text = Intent.GetStringExtra("fromCurr");
            exchangedVal.Text = Intent.GetStringExtra("exValue");
            picId = int.Parse(Intent.GetStringExtra("picId"));

            ImageView arrow = FindViewById<ImageView>(Resource.Id.arrow);
            arrow.SetImageResource(Resource.Drawable.arrow);

            spin = FindViewById<Spinner>(Resource.Id.submitSpinner);
            spin.Visibility = ViewStates.Invisible;

            Button getNames = FindViewById<Button>(Resource.Id.getNames);
            getNames.Click += GetNames_Click;

            Button submit = FindViewById<Button>(Resource.Id.Submit);
            submit.Click += Submit_Click;

            Button cancel = FindViewById<Button>(Resource.Id.Cancel);
            cancel.Click += Cancel_Click;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            var smartPriceAct = new Intent(Application.Context, typeof(SmartPriceActivity));
            StartActivity(smartPriceAct);
        }

        private async void Submit_Click(object sender, EventArgs e)
        {
            ProductDTO product = new ProductDTO();

            EditText Name = FindViewById<EditText>(Resource.Id.productName);
            EditText Description = FindViewById<EditText>(Resource.Id.productDescription);

            product.Product_Id = -1;
            product.Name = Name.Text;
            product.Description = Description.Text;

            PriceDTO price = new PriceDTO();

            EditText Shop = FindViewById<EditText>(Resource.Id.shop);

            price.Price_Id = -1;
            price.Shop = Shop.Text;
            price.ToCurrency = toCurr.Text;
            price.FromCurrency = fromCurr.Text;
            price.ExchangedValue = decimal.Parse(exchangedVal.Text);
            price.PicturePathId = picId;
            price.PriceToConvert = Intent.GetStringExtra("priceToConvert");
            if (spin.Visibility == ViewStates.Visible)
            {
                price.product = new ProductDTO();
                price.Product_Id = -1;
                price.product.Name = spinnerValue;
                price.product.Description = "";
            }
            else
                price.product = product;

            using (var client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(price);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(json, Encoding.UTF8, "application/json"));
                    var uri = Utils.baseUrl + "Price/Submit";
                    var result = await client.PostAsync(uri, content);

                    Toast.MakeText(this, "Sent successfully! ", ToastLength.Short).Show();

                    var smartPriceAct = new Intent(Application.Context, typeof(SmartPriceActivity));
                    StartActivity(smartPriceAct);
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }
        private void GetNames_Click(object sender, EventArgs e)
        {
            spin.Visibility = ViewStates.Visible;
            List<string> products = new List<string>();
            using (var client = new HttpClient())
            {
                Task<string> result = client.GetStringAsync("http://192.168.1.5/SmartPrice/api/Product/GetProductNames");
                products = JsonConvert.DeserializeObject<List<string>>(result.Result);
            }
            spin.Adapter = new MyListAdapter(Application.Context, products);
            spin.ItemSelected += Spin_ItemSelected;
        }

        private void Spin_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            spinnerValue = spinner.GetItemAtPosition(e.Position).ToString();
            string toast = string.Format("Selected value is {0}", spinnerValue);
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }

    public class MyListAdapter : BaseAdapter<string>
    {
        readonly LayoutInflater inflater;
        List<string> itemList;

        public MyListAdapter(Context context, List<string> items)
        {
            inflater = LayoutInflater.FromContext(context);
            itemList = items;
        }

        public override string this[int index]
        {
            get { return itemList[index]; }
        }

        public override int Count
        {
            get { return itemList.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //Switch your layout as you like it
            View view = inflater.Inflate(Resource.Layout.NameItem, parent, false);

            var item = itemList[position];

            view.FindViewById<TextView>(Resource.Id.nameSpinText).Text = item;
            return view;
        }
    }
}