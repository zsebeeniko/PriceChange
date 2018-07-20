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

namespace SmartPrice
{
    public class LatLong
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public LatLong(double lat, double lng)
        {
            this.Latitude = lat;
            this.Longitude = lng;
        }
    }
}