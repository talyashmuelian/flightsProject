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
        private const string AllFlightsURL = "https://data-cloud.flightradar24.com/zones/fcgi/feed.js?faa=1&bounds=32.366%2C31.271%2C34.012%2C36.282&satellite=1&mlat=1&flarm=1&adsb=1&gnd=1&air=1&vehicles=1&estimated=1&maxage=14400&gliders=1&stats=1";
        private const string FlightURL = "https://data-live.flightradar24.com/clickhandler/?version=1.5&flight=";
        private IDL dl=DLImp.theInstance();
        static BLImp instance;//=new DLImp();
        private BLImp()
        {
            
        }
        ~BLImp()
        {
            dl.DestroyThread();

                }
        static public BLImp theInstance()
        {
            if (instance == null)
                instance = new BLImp();
            return instance;
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
            Dictionary<string, List<FlightInfoPartial>> dict = new Dictionary<string, List<FlightInfoPartial>>();
            using (var webClient = new System.Net.WebClient())
            {

                var json =  webClient.DownloadString(AllFlightsURL);
                extractFlightFromString(json, dict);
            }
            return dict;
        }
        //}
        /// <summary>
        /// private async function, the public called her
        /// </summary>
        /// <param name="dict"></param>
        public async void GetCurrentFlightsAsync(Dictionary<string, List<FlightInfoPartial>> dict)
        {
            using (var webClient = new System.Net.WebClient())
            {
                var json = await webClient.DownloadStringTaskAsync(AllFlightsURL);
                extractFlightFromString(json, dict);
            }
                   }
        private void extractFlightFromString(string json, Dictionary<string, List<FlightInfoPartial>> dict)
        {
            List<FlightInfoPartial> incoming = new List<FlightInfoPartial>();
            List<FlightInfoPartial> outgoing = new List<FlightInfoPartial>();
            JObject allFlightData = null;
            dict.Clear();
            allFlightData = JObject.Parse(json);
            try
            {
                foreach (var item in allFlightData)
                {
                    var key = item.Key;
                    if (key == "full_count" || key == "version" || key == "stats" || key == "visible") continue;
                    if (item.Value[11].ToString() == "TLV")
                    {
                        outgoing.Add(new FlightInfoPartial
                        {
                            ID = -1,
                            Source = item.Value[11].ToString(),
                            Destination = item.Value[12].ToString(),
                            FlightID = key,
                            Long = Convert.ToDouble(item.Value[1]),
                            Lat = Convert.ToDouble(item.Value[2]),
                            DepartureTime = Util.Helper.GetDateFromEpoch(Convert.ToDouble(item.Value[10])),
                            FlightCode = item.Value[13].ToString()
                        });
                    }
                    if (item.Value[12].ToString() == "TLV")
                    {
                        incoming.Add(new FlightInfoPartial
                        {
                            ID = -1,
                            Source = item.Value[11].ToString(),
                            Destination = item.Value[12].ToString(),
                            FlightID = key,
                            Long = Convert.ToDouble(item.Value[1]),
                            Lat = Convert.ToDouble(item.Value[2]),
                            DepartureTime = Util.Helper.GetDateFromEpoch(Convert.ToDouble(item.Value[10])),
                            FlightCode = item.Value[13].ToString()
                        });
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            dict.Add("outgoing", outgoing);
            dict.Add("incoming", incoming);
        }
        #endregion
        public BE.Weather.Root GetWeather(double lon, double lat)
        {
            BE.Weather.Root result = null;
            string urlWhether = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=05b1f955b138af8db2dac1319b725a96";
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = webClient.DownloadString(urlWhether);
                    result = (BE.Weather.Root)JsonConvert.DeserializeObject(json, typeof(BE.Weather.Root));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return result;
        }

        public BE.Flights.Root GetSelectedFlight(string id)
        {
            BE.Flights.Root result = null;
            using (var webClient = new System.Net.WebClient())
            {
                try
                {
                    var json = webClient.DownloadString(FlightURL+id);
                    result = (BE.Flights.Root)JsonConvert.DeserializeObject(json, typeof(BE.Flights.Root));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return result;
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
                        where f.CreateTime > start && f.CreateTime < end
                        select f).ToList();
            }
            catch { return null; }

        }
    }
   
    
}