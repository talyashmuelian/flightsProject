using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Flights;
using BE.Weather;
using DL;
using Newtonsoft.Json;

namespace BL
{
    public class BLImp:IBL
    {
        private const string AllFlightsURL = "https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=38.805%2C24.785%2C29.014%2C40.505&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";
        private const string FlightURL = "https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";
        private IDL dl=DLImp.theInstance();
        static BLImp instance;//=new DLImp();
        private BLImp()
        {
            
        }
        //~BLImp()
        //{
        //    dl.DestroyThread();

        //        }
        static public BLImp theInstance()
        {
            if (instance == null)
                instance = new BLImp();
            return instance;
        }
        public void DestroyThread()
        {
            dl.DestroyThread();
        }
        //GetCurrentFlights-לטפל לגבי סינכרוני ואסינכרוני
        #region GetCurrentFlights
        //public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlights()
        //{
        //    Dictionary<string, List<FlightInfoPartial>> result = new Dictionary<string, List<FlightInfoPartial>>();
        //    GetCurrentFlightsAsync(result);//call the async function
        //    return result;
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlightsSync()
        {
            var flights =  dl.GetCurrentFlightsSync();
            return seperateFlights(flights);
        }
        //}
        /// <summary>
        /// private async function, the public called her
        /// </summary>
        /// <param name="dict"></param>
        public async Task<Dictionary<string, List<FlightInfoPartial>>> GetCurrentFlightsAsync(Dictionary<string, List<FlightInfoPartial>> dict)
        {
           var flights= await dl.GetCurrentFlightsAsync();
           return seperateFlights(flights);
            }
        private Dictionary<string, List<FlightInfoPartial>> seperateFlights(List<FlightInfoPartial> flights)
        {
            Dictionary<string, List<FlightInfoPartial>> dict = new Dictionary<string, List<FlightInfoPartial>>();
            List<FlightInfoPartial> incoming = new List<FlightInfoPartial>();
            List<FlightInfoPartial> outgoing = new List<FlightInfoPartial>();
            try
            {
                foreach (var item in flights)
                {
                    
                    if (item.Source == "TLV")
                    {
                        outgoing.Add(item);
                    }
                    else if (item.Destination == "TLV")
                    {
                        incoming.Add(item);
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            dict.Add("outgoing", outgoing);
            dict.Add("incoming", incoming);
            return dict;
        }
        #endregion
        public  BE.Weather.Root GetWeather(double lon, double lat)
        {
            return  dl.GetWeather(lon, lat);
        }

        public  BE.Flights.Root GetSelectedFlight(string id)
        {
            return  dl.GetSelectedFlight(id);
        //{
        //    BE.Flights.Root result = null;
        //    using (var webClient = new System.Net.WebClient())
        //    {
        //        try
        //        {
        //            var json = webClient.DownloadString(FlightURL+id);
        //            result = (BE.Flights.Root)JsonConvert.DeserializeObject(json, typeof(BE.Flights.Root));
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e);
        //        }
        //    }
        //    return result;
        }

        public void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial)
        {
            dl.SaveFlightInfoPartial(flightInfoPartial);
        }

        public List<FlightInfoPartial> GetSavedFlights(DateTime start, DateTime end)
        {
            try
            {
                return (from f in dl.GetSavedFlights()
                        where f.CreateTime >= start && f.CreateTime <= end
                        select f).ToList();
            }
            catch { return null; }

        }

        public async Task<bool> IsBeforeHoliday(DateTime date)
        {
            return await dl.IsBeforeHoliday(date);
            //var yyyy = date.ToString("yyyy");
            //var mm = date.ToString("MM");
            //var dd = date.ToString("dd");
            //string hebrewDateUrl = $"https://www.hebcal.com./converter?cfg=json&date={yyyy}-{mm}-{dd}&g2h=1&strict=1";
            //BE.HebrewDates.Root root = null;
            //try
            //{
            //    using (var webClient = new System.Net.WebClient())
            //    {
            //        try
            //        {
            //            var json = await webClient.DownloadStringTaskAsync(hebrewDateUrl);
            //            root = (BE.HebrewDates.Root)JsonConvert.DeserializeObject(json, typeof(BE.HebrewDates.Root));
            //        }
            //        catch (Exception e)
            //        {
            //            Console.WriteLine(e);
            //        }
            //    }
            //    return root.events[0].Contains("Erev");
            //}
            //catch { return false; }

        }

        public bool IsBeforeHoliday1(DateTime date)
        {
            return dl.IsBeforeHoliday1(date);
        }
    }
   
    
}