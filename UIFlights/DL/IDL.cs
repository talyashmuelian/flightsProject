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
        void DestroyThread();
        List<FlightInfoPartial> GetCurrentFlightsSync();
        Task<List<FlightInfoPartial>> GetCurrentFlightsAsync();

        BE.Weather.Root GetWeather(double lon, double lat);
        BE.Flights.Root GetSelectedFlight(string id);
        Task<bool> IsBeforeHoliday(DateTime date);
        bool IsBeforeHoliday1(DateTime date);

    }
}
