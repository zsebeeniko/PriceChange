﻿using System;

namespace SmartPrice
{
    static class Utils
    {
        public static string baseUrl = "http://192.168.1.5/SmartPrice/api/";
        public enum DistanceUnit { Miles, Kilometers };
        public static double ToRadian(this double value)
        {
            return (Math.PI / 180) * value;
        }
        public static double HaversineDistance(LatLong coord1, LatLong coord2, DistanceUnit unit)
        {
            double R = (unit == DistanceUnit.Miles) ? 3960 : 6371;
            var lat = (coord2.Latitude - coord1.Latitude).ToRadian();
            var lng = (coord2.Longitude - coord1.Longitude).ToRadian();

            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                     Math.Cos(coord1.Latitude.ToRadian()) * Math.Cos(coord2.Latitude.ToRadian()) *
                     Math.Sin(lng / 2) * Math.Sin(lng / 2);

            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));

            return R * h2;
        }
    }
}