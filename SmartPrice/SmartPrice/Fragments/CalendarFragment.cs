using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using SmartPrice.BL.BusinessLayerContracts.DTOs;
using System;
using System.Drawing;
using System.Globalization;

namespace SmartPrice.Fragments
{
    public class CalendarFragment : Fragment
    {
        private ListView lv;
        private ProductAdapter adapter;
        Context context;
        ProductsViewModel pvm;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.CalendarFragment, container, false);
            context = view.Context;

            lv = view.FindViewById<ListView>(Resource.Id.productsListCalendar);
            lv.Visibility = ViewStates.Invisible;

            CalendarView calendar = view.FindViewById<CalendarView>(Resource.Id.calendar);
            calendar.DateChange += Calendar_DateChange;
            pvm = new ProductsViewModel();

            lv.Adapter = adapter;
            lv.ItemClick += Lv_ItemClick;
            return view;
        }

        private void Calendar_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            lv.Visibility = ViewStates.Visible;
            string day = "";
            string month = "";

            if (e.DayOfMonth < 10)
                day = "0" + e.DayOfMonth.ToString();
            else
                day = e.DayOfMonth.ToString();
            int monthInt = e.Month + 1;
            if (e.Month < 10)
                month = "0" + monthInt.ToString();
            else
                month = monthInt.ToString();

            string changedDate = e.Year.ToString() + "-" + month + "-" + day ;
            DateTime date = DateTime.ParseExact(changedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            adapter = new ProductAdapter(context, pvm.FilteredProducts(date));
            lv.Adapter = adapter;
        }

        private void Lv_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            PriceDTO item = adapter.GetItemById(e.Position);
            pvm.DisplayAlert(item, context);
        }
    }
}