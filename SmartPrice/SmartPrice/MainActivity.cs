using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Support.V4.App;
using Java.Lang;
using com.refractored;
using Android.Support.V4.View;

namespace SmartPrice
{
    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
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

        }

        public class MyAdapter : FragmentPagerAdapter
        {
            int tabCount = 3;
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
                else if (position == 1)
                {
                    cs = new Java.Lang.String("ProductList");
                }
                else
                {
                    cs = new Java.Lang.String("Rest Test");
                }

                return cs;
            }

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                return ContentFragment.NewInstance(position);
            }
        }
    }
}

