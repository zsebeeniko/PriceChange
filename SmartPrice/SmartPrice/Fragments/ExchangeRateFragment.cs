﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SmartPrice.Fragments
{
    public class ExchangeRateFragment : Fragment
    {
        Context context;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.ExchangeFragment, container, false);
            context = view.Context;

            LinearLayout layout = view.FindViewById<LinearLayout>(Resource.Id.exchange);
            Steema.TeeChart.TChart chart = new Steema.TeeChart.TChart(context);
            chart.Header.Text = "Exchange-rate variation"; 

            Steema.TeeChart.Styles.Line euro = new Steema.TeeChart.Styles.Line();
            Steema.TeeChart.Styles.Line dollar = new Steema.TeeChart.Styles.Line();
            chart.Series.Add(euro);
            chart.Series.Add(dollar);

            euro.Title = "Euro";
            dollar.Title = "Dollar";

            euro.Add(3, "Monday");
            euro.Add(5, "Tuesday");
            euro.Add(6, "Wednesday");
            dollar.Add(6, "Today");
            dollar.Add(12, "Monday");

            Steema.TeeChart.Themes.BlackIsBackTheme theme = new Steema.TeeChart.Themes.BlackIsBackTheme(chart.Chart);
            theme.Apply();

            euro.Color = Color.Orange;
            dollar.Color = Color.OrangeRed;

            //chart.Header.Color = Color.OrangeRed;
            chart.Header.Font.Color = Color.OrangeRed;
            layout.AddView(chart, new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, 600));
            return view;
        }
    }
}