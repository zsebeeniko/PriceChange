﻿using Android.App;
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

namespace SmartPriceTest
{
    public static class App
    {
        public static File _file;
        public static File _dir;
        public static Bitmap bitmap;
    }

    [Activity(Label = "@string/app_name", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        MyAdapter myAdapter;
        PagerSlidingTabStrip tabs;
        ViewPager pager;
        ImageView _imageView;

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

        private void CreateDirectoryForPictures()
        {
            App._dir = new File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Make it available in the gallery


            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            // Display in ImageView. We will resize the bitmap to fit the display
            // Loading the full sized image will consume to much memory 
            // and cause the application to crash.

            int height = Resources.DisplayMetrics.HeightPixels;
            int width = _imageView.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if (App.bitmap != null)
            {
                _imageView.SetImageBitmap(App.bitmap);
                App.bitmap = null;
            }

            // Dispose of the Java side bitmap.
            GC.Collect();

        }

        public void TakePhoto(Intent intent, ImageView imageview)
        {
            _imageView = imageview;
            StartActivityForResult(intent, 0);
        }
    }
}

