using BE.Flights;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UIFlights
{
    public class FlightsModel
    {
        public Dictionary<string,List<FlightInfoPartial>> Flights { get; set; }
        private BLImp bl = BLImp.theInstance();
        public FlightsModel()
        {
            startClock();
        }
        public void startClock()
        {
            while (true){
                Flights=bl.GetCurrentFlights();
                Thread.Sleep(10000);
            }
        }

    }
}
