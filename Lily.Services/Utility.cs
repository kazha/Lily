using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lily.Services
{
    public static class Utility
    {
        public static double DistanceFrom(double latFrom,double longFrom,double latTo, double longTo)
        {
            var p = Math.PI / 180;
            var a = 0.5 - Math.Cos((latFrom - latTo) * p) / 2 + Math.Cos(latTo * p) * Math.Cos(latFrom * p) * (1 - Math.Cos((longFrom - longTo) * p)) / 2;
            var result = 12742 * Math.Asin(Math.Sqrt(a)); // 2 * R; R = 6371 km
            return Math.Round(result, 2, MidpointRounding.AwayFromZero);
        }
    }
}
