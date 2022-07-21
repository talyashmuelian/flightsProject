using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BE.Flights;
using BL;
using Newtonsoft.Json;

namespace UIFlights
{
   public class FlightModel
    {
        private BE.Flights.Root flightRoot;
        private BE.Weather.Root rootWeatherOrigin;
        private BE.Weather.Root rootWeatherDestination;


        private BLImp bl = BLImp.theInstance();

        public string DestinationName
        {
            get
            {
                try{
                    return flightRoot.airport.destination.name;}
                catch{ return "NA"; }
            }
        }

        public string DestinationCountryName 
        {
            get {
                try
                {
                    return flightRoot.airport.destination.position.country.name;
                }
                catch
                { 
                    return "NA"; }
                }
        }
        public string FlightId
        {
            get {
                try
                {
                    return flightRoot.identification.id;
                }
                catch
                {
                    return "NA";
                }
            
        }
        }
        public string AirlineName
        {
            get
            {
                try
                {
                    return flightRoot.airline.name;
                }
                catch
                {
                    return "NA";
                }
            }
        }
            
        public string OriginName
        {
            get
            {
                try
                {
                    return flightRoot.airport.origin.name;
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string OriginCountryName
        {
            get
            {
                try
                {
                    return flightRoot.airport.origin.position.country.name;
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string Status
        {
            get
            {
                try
                {
                    return flightRoot.status.text;
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string EstimetedArrivalTime
        {
            get
            {
                try
                {
                    var arrival = flightRoot.time.estimated.arrival;
                    if (arrival != null)
                        return Util.Helper.GetDateFromEpoch(Convert.ToDouble(arrival)).ToString();
                    else { return "NA"; }
                }
                catch
                {
                    return  "NA";
                }
            }
        }
        public string DepartureTime
        {
            get
            {
                try
                {
                    var departure = flightRoot.time.estimated.departure;
                    if (departure != null)
                        return Util.Helper.GetDateFromEpoch(Convert.ToDouble(departure)).ToString();
                    else { return "NA"; }
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string DestinationWeather
        {
            get
            {
                try
                {
                    return Util.Helper.kelvinToCelsius(rootWeatherDestination.main.temp).ToString();
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string OriginWeather
        {
            get
            {
                try
                {
                    return Util.Helper.kelvinToCelsius(rootWeatherOrigin.main.temp).ToString();
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string DescriptionOrigin { 
            get
            {
                try
                {
                    return rootWeatherOrigin.weather[0].description;
                }
                catch { return "NA"; }
            }
        }
        public string DescriptionDestination
        {
            get
            {
                try
                {
                    return rootWeatherDestination.weather[0].description;
                }
                catch { return "NA"; }
            }
        }
        public List<Trail> Trail {
            get
            {
                try
                {
                    return flightRoot.trail;
                }
                catch { return new List<Trail>(); }

            }
        }

        public FlightModel(string id)
        {
            flightRoot = bl.GetSelectedFlight(id);
            try
            {
                double lonOrigin = flightRoot.airport.origin.position.longitude;
                double latOrigin = flightRoot.airport.origin.position.latitude;
                rootWeatherOrigin = bl.GetWeather(lonOrigin, latOrigin);
            }
            catch { }
            try
            {
                double lonDes = flightRoot.airport.destination.position.longitude;
                double latDes = flightRoot.airport.destination.position.latitude;
                rootWeatherDestination = bl.GetWeather(lonDes, latDes);
            }
            catch { }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}
