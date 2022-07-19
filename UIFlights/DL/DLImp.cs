using BE.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DL
{
    public class DLImp:IDL
    {
        static DLImp instance;//=new DLImp();
        Thread saveThread;
        List<FlightInfoPartial> flightToSave = new List<FlightInfoPartial>();
        private DLImp()
        {
            saveThread = new Thread(runThread);
            saveThread.Start();
        }
        //~DLImp()
        //{
        //    Console.WriteLine("dsjkfhcsdk");
        //    saveToDB();
        //    saveThread.Abort();
        //}
        private void runThread()
        {
            while (true)
            {
                saveToDB();
                Thread.Sleep(10000);
            }
        }
        private void saveToDB()
        {
            if (flightToSave.Count() > 0)
            {
                object lck = new object();
                lock (lck)
                {
                    using (var ctx = new FlightContext())
                    {
                        foreach (var f in flightToSave)
                        {
                            ctx.Flights.Add(f);
                            ctx.SaveChanges();
                        }
                    }
                    flightToSave.Clear();
                }
            }
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
           saveToDB();
           // Thread.Sleep(15000);
            using (var ctx = new FlightContext())
            {
                return (from f in ctx.Flights
                               select f).ToList();
            }
        }

        public void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial)
        {
             flightToSave.Add(flightInfoPartial);
            //using (var ctx = new FlightContext())
            //{
            //    ctx.Flights.Add(flightInfoPartial);
            //    ctx.SaveChanges();
            //}
        }

        public void DestroyThread()
        {
            saveToDB();
            saveThread.Abort();
        }
    }
}
