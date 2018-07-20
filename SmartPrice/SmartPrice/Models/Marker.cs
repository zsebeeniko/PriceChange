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

namespace SmartPrice.Models
{
    public class Marker
    {
        public string Title { get; set; }
        public double Lattitude { get; set; }
        public double Longitude { get; set; }

        public Marker (string title, double lat, double lng)
        {
            this.Title = title;
            this.Lattitude = lat;
            this.Longitude = lng;
        }
    }
}