using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE.Flights;
namespace consoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BLImp bl = BLImp.theInstance();

            Console.WriteLine(bl.GetWhether(34,32));
            Console.WriteLine(bl.GetCurrentFlights());
            foreach(var item in bl.GetCurrentFlights())
            {
                foreach(var item2 in item.Value)
                    Console.WriteLine(item2);
            }
            Console.ReadLine();
        }
    }
}
