using System;
using System.Collections.Generic;
using System.Text;
using NetTopologySuite.Geometries;

namespace NextToMe.Common
{
    public static class DistanceConverter
    {
        public static double DistanceInMeters(Point point1, Point point2)
        {
            var latitude1 = point1.X;
            var longitude1 = point1.Y;
            var latitude2 = point2.X;
            var longitude2 = point2.Y;

            double theta = longitude1 - longitude2;
            double dist = Math.Sin(DegreesToRadians(latitude1)) * Math.Sin(DegreesToRadians(latitude2)) + Math.Cos(DegreesToRadians(latitude1)) * Math.Cos(DegreesToRadians(latitude1)) * Math.Cos(DegreesToRadians(theta));
            dist = Math.Acos(dist);
            dist = RadiansToDegrees(dist);
            dist = dist * 111189.57697;
            return (dist);
        }

        private static double DegreesToRadians(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        private static double RadiansToDegrees(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
