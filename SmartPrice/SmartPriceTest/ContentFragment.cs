using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;

namespace SmartPriceTest
{
    public class ContentFragment : Fragment
    {
        private int position;

        public static ContentFragment NewInstance(int position)
        {
            var f = new ContentFragment();
            var b = new Bundle();
            b.PutInt("position", position);
            f.Arguments = b;
            return f;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            position = Arguments.GetInt("position");
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var root = inflater.Inflate(Resource.Layout.CameraFragment, container, false);
            var text = root.FindViewById<TextView>(Resource.Id.textView);

            if (position == 0)
                text.Text = "Camera Page";
            else
                text.Text = "Product Lists";

            ViewCompat.SetElevation(root, 50);
            return root;
        }
    }
}