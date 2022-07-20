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
        void GetCurrentFlightsAsync(Dictionary<string, List<FlightInfoPartial>> dict);

        BE.Weather.Root GetWeather(double lon, double lat);
        BE.Flights.Root GetSelectedFlight(string id);
        void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial);
        List<FlightInfoPartial> GetSavedFlights(DateTime start, DateTime end);
        void DestroyThread();
    }
}
