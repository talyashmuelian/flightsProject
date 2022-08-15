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
    public class BLImp : IBL
    {

        private IDL dl = DLImp.theInstance();
        static BLImp instance;
        private BLImp()
        {

        }
        static public BLImp theInstance()
        {
            if (instance == null)
                instance = new BLImp();
            return instance;
        }
        /// <summary>
        /// keep all Israel holiday and their date
        /// </summary>
        private readonly Dictionary<Tuple<string, int>, string> israelHolidays = new Dictionary<Tuple<string, int>, string>() {
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

        /// <summary>
        /// return a dictionary of incoming/outgoing flights synchronously
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<FlightInfoPartial>> GetCurrentFlightsSync()
        {
            var flights = dl.GetCurrentFlightsSync();
            return seperateFlights(flights);
        }

        /// <summary>
        ///  return a dictionary of incoming/outgoing flights asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, List<FlightInfoPartial>>> GetCurrentFlightsAsync()
        {
            var flights = await dl.GetCurrentFlightsAsync();
            return seperateFlights(flights);
        }

        /// <summary>
        /// get a list of all flights from/to tel aviv, and seperate them to incoming and outgoing
        /// </summary>
        /// <param name="flights"></param>
        /// <returns></returns>
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

        /// <summary>
        /// return the full information about the required flight
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BE.Flights.Root> GetSelectedFlightAsync(string id)
        {
            return await dl.GetSelectedFlightAsync(id);
        }

        /// <summary>
        /// get specific flight and seve it to data base
        /// </summary>
        /// <param name="flightInfoPartial"></param>
        public void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial)
        {
            dl.SaveFlightInfoPartial(flightInfoPartial);
        }

        /// <summary>
        /// return all the flights from the data base in the required range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
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

        /// <summary>
        /// return pair of values, if we are before holiday in the 6 days next, and which holiday is upcoming
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> IsBeforeHoliday(DateTime date)
        {
            Tuple<bool, string> result = Tuple.Create(false, "");
            for (int i = 0; i < 7; i++)
            {//check all the 6 next days
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

        /// <summary>
        /// get the weather of specific place asynchronously
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public async Task<BE.Weather.Root> GetWeatherAsync(double lon, double lat)
        {
            return await dl.GetWeatherAsync(lon, lat);
        }

    }
}