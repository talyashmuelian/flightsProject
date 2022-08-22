using BE.Flights;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BE;

namespace DL
{
    public class DLImp : IDL
    {
        private const string AllFlightsURL = "https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=71.896%2C-81.448%2C-180%2C180&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&airport=TLV&stats=1";
        private const string FlightURL = "https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";
        static DLImp instance;//=new DLImp();
        private DLImp()
        {
        }
        static public DLImp theInstance()
        {
            if (instance == null)
                instance = new DLImp();
            return instance;
        }
        /// <summary>
        /// return all the saved flights from the data base
        /// </summary>
        /// <returns></returns>
        public List<FlightInfoPartial> GetSavedFlights()
        {
            try
            {
                using (var ctx = new FlightContext())
                {
                    return (from f in ctx.Flights
                            select f).ToList();
                }
            }
            catch { return null; }
        }

        /// <summary>
        /// save to data base the required flight
        /// </summary>
        /// <param name="flightInfoPartial"></param>
        public void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial)
        {
            try
            {
                using (var ctx = new FlightContext())
                {
                    ctx.Flights.Add(flightInfoPartial);
                    ctx.SaveChanges();
                }
            }
            catch { }
        }

        /// <summary>
        /// retrurn all the current flights from/to tel aviv synchronously
        /// </summary>
        /// <returns></returns>
        public List<FlightInfoPartial> GetCurrentFlightsSync()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = webClient.DownloadString(AllFlightsURL);
                return extractFlightFromString(json);
            }
        }
        /// <summary>
        /// get a string in json  format an d ectract from it a list of FlightInfoPartial
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private List<FlightInfoPartial> extractFlightFromString(string json)
        {
            List<FlightInfoPartial> flights = new List<FlightInfoPartial>();
            JObject allFlightData = null;
            allFlightData = JObject.Parse(json);
            try
            {
                foreach (var item in allFlightData)
                {
                    var key = item.Key;
                    if (key == "full_count" || key == "version" || key == "stats" || key == "visible") continue;
                    if (item.Value[11].ToString() == "TLV" || item.Value[12].ToString() == "TLV")
                    {
                        flights.Add(new FlightInfoPartial
                        {
                            ID = -1,
                            Source = item.Value[11].ToString(),
                            Destination = item.Value[12].ToString(),
                            FlightID = key,
                            Long = Convert.ToDouble(item.Value[2]),
                            Lat = Convert.ToDouble(item.Value[1]),
                            DepartureTime = Util.Helper.GetDateFromEpoch(Convert.ToDouble(item.Value[10])),
                            FlightCode = item.Value[13].ToString(),
                            Angle = Convert.ToDouble(item.Value[3])
                        });
                    }
                }
            }
            catch { }
            if (flights.Count == 0)//if the server dose'nt return any flight
                throw new NoDataException("no flights return from server");
            return flights;
        }
        /// <summary>
        /// retrurn all the current flights from/to tel aviv asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<List<FlightInfoPartial>> GetCurrentFlightsAsync()
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(AllFlightsURL);
                return extractFlightFromString(json);
            }
        }
        /// <summary>
        /// return the full information about the required flight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BE.Flights.Root> GetSelectedFlightAsync(string id)
        {
            BE.Flights.Root result = null;
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    var json = await webClient.DownloadStringTaskAsync(FlightURL + id);
                    result = (BE.Flights.Root)JsonConvert.DeserializeObject(json, typeof(BE.Flights.Root));
                }
            }
            catch { }
            return result;
        }

        /// <summary>
        /// get the weather of specific place asynchronously
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public async Task<BE.Weather.Root> GetWeatherAsync(double lon, double lat)
        {
            BE.Weather.Root result = null;
            string urlWhether = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=05b1f955b138af8db2dac1319b725a96";
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    var json = await webClient.DownloadStringTaskAsync(urlWhether);
                    result = (BE.Weather.Root)JsonConvert.DeserializeObject(json, typeof(BE.Weather.Root));
                }
            }
            catch { }
            return result;
        }


        /// <summary>
        /// return information about specific date asynchronously
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<BE.HebrewDates.Root> GetHebrewDate(DateTime date)
        {
            var yyyy = date.ToString("yyyy");
            var mm = date.ToString("MM");
            var dd = date.ToString("dd");
            string hebrewDateUrl = $"http://www.hebcal.com./converter?cfg=json&date={yyyy}-{mm}-{dd}&g2h=1&strict=1";
            BE.HebrewDates.Root root = null;
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    var json = await webClient.DownloadStringTaskAsync(hebrewDateUrl);
                    root = (BE.HebrewDates.Root)JsonConvert.DeserializeObject(json, typeof(BE.HebrewDates.Root));
                }
                return root;
            }
            catch { return null; }
        }
    }
}
