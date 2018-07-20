using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmartPrice.Models;

namespace SmartPrice.VieModels
{
    public class MarkersViewModel
    {
        private List<MarkerOptions> markers;

        public List<MarkerOptions> Markers => GetMarkers();

        private List<MarkerOptions> GetMarkers()
        {
            if (markers == null)
                markers = new List<MarkerOptions>();

            return markers;
        }

        public List<MarkerOptions> AddMarker(Models.Marker marker)
        {
            if (markers == null)
            {
                markers = new List<MarkerOptions>();
                MarkerOptions markerOptions = new MarkerOptions();
                markerOptions.SetPosition(new LatLng(16.03, 108));
                markerOptions.SetTitle("MyPosition");
                markers.Add(markerOptions);
            }
            MarkerOptions markerOption = new MarkerOptions();
            markerOption.SetTitle(marker.Title);
            markerOption.SetPosition(new LatLng(marker.Lattitude, marker.Longitude));
            markers.Add(markerOption);

            return markers;
        }
    }
}