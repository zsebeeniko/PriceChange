using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;

namespace SmartPriceTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView lv;
        private ProductAdapter adapter;
        private JavaList<Product> products;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            lv = FindViewById<ListView>(Resource.Id.productsList);
            adapter = new ProductAdapter(this, GetProducts());

            lv.Adapter = adapter;
            lv.ItemClick += Lv_ItemClick;

        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this, products[e.Position].Name, ToastLength.Short).Show();
        }

        private JavaList<Product> GetProducts()
        {
            products = new JavaList<Product>();
            Product p;

            p = new Product("Picture 1", Resource.Drawable.pic1);
            products.Add(p);

            p = new Product("Picture 2", Resource.Drawable.pic2);
            products.Add(p);

            p = new Product("Picture 3", Resource.Drawable.pic3);
            products.Add(p);

            p = new Product("Picture 4", Resource.Drawable.pic4);
            products.Add(p);

            return products;
        }
    }
}

