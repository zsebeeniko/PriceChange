using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SmartPrice.VieModels;

namespace SmartPrice.Fragments
{
    public class MapsFragment : Fragment, IOnMapReadyCallback
    {
        MapView mapView;
        public GoogleMap googleMap;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        { 
            var view = inflater.Inflate(Resource.Layout.MapFragment, container, false);
            mapView = view.FindViewById<MapView>(Resource.Id.map);
            mapView.OnCreate(savedInstanceState);
            mapView.OnResume();

            try
            {
                MapsInitializer.Initialize(Activity.ApplicationContext);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

            mapView.GetMapAsync(this);
            return view;
        }

        public void OnMapReady(GoogleMap mMap)
        {
            googleMap = mMap;            
            AddMarkers(googleMap, HomeFragment.instance.MarkersViewModelInstance().Markers);

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());

        }

        private void AddMarkers(GoogleMap map, List<MarkerOptions> markers)
        {
            foreach (MarkerOptions marker in markers)
            {
                map.AddMarker(marker);
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }

        public override void OnPause()
        {
            base.OnPause();
            mapView.OnPause();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            mapView.OnDestroy();
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
            mapView.OnLowMemory();
        }
    }
}