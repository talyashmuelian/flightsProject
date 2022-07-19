using BE.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class DLImp:IDL
    {
        static DLImp instance;//=new DLImp();
        private DLImp()
        {
               
        }
        //private async void initDispatcherTimer()
        //{
        //    DispatcherTimer dispatcherTimer = new DispatcherTimer();
        //    dispatcherTimer.Tick += DispatcherTimer_Tick_Flights;
        //    dispatcherTimer.Tick += DispatcherTimer_Tick_Flight;
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
        //    dispatcherTimer.Start();
        //}
        static public DLImp theInstance()
        {
            if (instance == null)
                instance = new DLImp();
            return instance;
        }

        public List<FlightInfoPartial> GetSavedFlights()
        {
            using (var ctx = new FlightContext())
            {
                return (from f in ctx.Flights
                               select f).ToList();
            }
        }

        public void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial)
        {
            using(var ctx =new FlightContext())
            {
                ctx.Flights.Add(flightInfoPartial);
                ctx.SaveChanges();
            }
        }
    }
}
