using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE.Flights;
using BE.Weather;
namespace BL
{
    public interface IBL
    {
        Dictionary<string, List<FlightInfoPartial>> GetCurrentFlightsSync();
        Task<Dictionary<string, List<FlightInfoPartial>>> GetCurrentFlightsAsync();

       
        Task<BE.Weather.Root> GetWeatherAsync(double lon, double lat);
        Task<BE.Flights.Root> GetSelectedFlightAsync(string id);
        void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial);
        List<FlightInfoPartial> GetSavedFlights(DateTime start, DateTime end);
        
        Task<Tuple<bool, string>> IsBeforeHoliday(DateTime date);

    }
}
