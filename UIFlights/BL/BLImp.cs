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
        private readonly Dictionary<Tuple<string,int>, string> israelHolidays = new Dictionary<Tuple<string, int>, string>() {
            {Tuple.Create("Tishrei",1), "rosh hashana" },
             {Tuple.Create("Tishrei",15), "sukot" },
            {Tuple.Create("Tishrei",22), "Shmini Atzeret" },
            {Tuple.Create("Kislev",25), "Hanucka" },
            {Tuple.Create("Tevet",2), "Hanucka" },
            {Tuple.Create("Adar",14), "Purim" },
            {Tuple.Create("Nisan",15), "Pesach" },
            {Tuple.Create("Nisan",22), "Pesach" },
            {Tuple.Create("Sivan",6), "Shavuot" },
        };
        private IDL dl=DLImp.theInstance();
        static BLImp instance;//=new DLImp();

        private BLImp()
        {
            
        }
        static public BLImp theInstance()
        {
            if (instance == null)
                instance = new BLImp();
            return instance;
        }
        #region GetCurrentFlights
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlightsSync()
        {
            var flights =  dl.GetCurrentFlightsSync();
            return seperateFlights(flights);
        }
        public async Task<Dictionary<string, List<FlightInfoPartial>>> GetCurrentFlightsAsync()
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

            catch { }
            dict.Add("outgoing", outgoing);
            dict.Add("incoming", incoming);
            return dict;
        }
        #endregion

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

        public async Task<Tuple<bool, string>> IsBeforeHoliday(DateTime date)
        {
            Tuple<bool, string> result= Tuple.Create(false, "");
            for (int i = 0; i < 7; i++) {
                var hebrewDate = await dl.GetHebrewDate(date.AddDays(i));
                foreach (var holiday in israelHolidays)
                {
                    try
                    {
                        if (hebrewDate.hm == holiday.Key.Item1 && hebrewDate.hd == holiday.Key.Item2)
                        {
                            result = Tuple.Create(true, holiday.Value);
                            break;
                        }
                    }
                    catch { }
                }
            }
            return result;

        }


        public async Task<BE.Weather.Root> GetWeatherAsync(double lon, double lat)
        {
            return await dl.GetWeatherAsync(lon, lat);
        }

        public async Task<BE.Flights.Root> GetSelectedFlightAsync(string id)
        {
            return await dl.GetSelectedFlightAsync(id);
        }
    }
   
    
}