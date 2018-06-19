using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;
using com.refractored;
using Android.Support.V4.View;

namespace SmartPriceTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private ListView lv;
        private ProductAdapter adapter;
        private JavaList<Product> products;
        MyAdapter myAdapter;
        PagerSlidingTabStrip tabs;
        ViewPager pager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            myAdapter = new MyAdapter(SupportFragmentManager);
            pager = FindViewById<ViewPager>(Resource.Id.pager);
            tabs = FindViewById<PagerSlidingTabStrip>(Resource.Id.tabs);

            pager.Adapter = myAdapter;
            tabs.SetViewPager(pager);
            tabs.SetBackgroundColor(Android.Graphics.Color.Aqua);


            //lv = FindViewById<ListView>(Resource.Id.productsList);
            //adapter = new ProductAdapter(this, GetProducts());

            //lv.Adapter = adapter;
            //lv.ItemClick += Lv_ItemClick;

        }

        public class MyAdapter : FragmentPagerAdapter
        {
            int tabCount = 2;
            public MyAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
            {

            }
            public override int Count
            {
                get
                {
                    return tabCount;
                }
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                ICharSequence cs;
                if (position == 0)
                    cs = new Java.Lang.String("Camera");
                else
                {
                    cs = new Java.Lang.String("ProductList");
                }

                return cs;
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                return ContentFragment.NewInstance(position);
            }
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

