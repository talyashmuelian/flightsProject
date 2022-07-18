using BE.Flights;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UIFlights
{
    public class FlightsModel
    {
        public Dictionary<string,List<FlightInfoPartial>> Flights { get; set; }
        private BLImp bl = BLImp.theInstance();
        
        public FlightsModel()
        {
          //  Flights = bl.GetCurrentFlightsAsync();
            //Thread thread = new Thread(startClock);
            //thread.Start();
            
        }
        
        public void startClock()
        {    
            while (true){
                //Flights=bl.GetCurrentFlights();
                //Flights["outgoing"].Add(new FlightInfoPartial{Destination= "jer" ,Source="san fransisco", ID=123 });
                //Thread.Sleep(100);
                //Flights["outgoing"].Add(new FlightInfoPartial { Destination = "maale", Source = "san fransisco", ID = 123 });

            }
        }

    }
}
