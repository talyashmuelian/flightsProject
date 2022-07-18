using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Helper
    {
        public static DateTime GetDateFromEpoch(double epochTime)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return start.AddSeconds(epochTime);
        }
        public static double kelvinToCelsius(double kelvin)
        {
            return kelvin-273.15;
        }
    }
}
