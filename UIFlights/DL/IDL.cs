using BE.Flights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public interface IDL
    {
        void SaveFlightInfoPartial(FlightInfoPartial flightInfoPartial);
        List<FlightInfoPartial> GetSavedFlights();
        List<FlightInfoPartial> GetCurrentFlightsSync();
        Task<List<FlightInfoPartial>> GetCurrentFlightsAsync();
        Task<BE.Weather.Root> GetWeatherAsync(double lon, double lat);
        Task<BE.Flights.Root> GetSelectedFlightAsync(string id);
        Task<BE.HebrewDates.Root> GetHebrewDate(DateTime date);

    }
}
