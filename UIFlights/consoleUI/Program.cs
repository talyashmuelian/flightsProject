using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE.Flights;
using UIFlights;

namespace consoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BLImp bl = BLImp.theInstance();
            FlightModel f = new FlightModel("2cb19464");
            Console.WriteLine(f);
            Console.WriteLine(bl.GetSelectedFlight("2cb1781a"));
            Console.WriteLine(bl.GetWeather(0, 0));
            Console.WriteLine(bl.GetCurrentFlightsSync());
            foreach (var item in bl.GetCurrentFlightsSync())
            {
                foreach (var item2 in item.Value)
                    Console.WriteLine(item2);
            }
            Console.ReadLine();
        }
    }
}
