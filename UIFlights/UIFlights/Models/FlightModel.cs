using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
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
                catch{ return ""; }
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
                    return ""; }
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
                    return "";
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
                    return "";
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
                    return "";
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
                    return "";
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
                    return "";
                }
            }
        }
        public DateTime EstimetedArrivalTime
        {
            get
            {
                try
                {
                    return Util.Helper.GetDateFromEpoch(Convert.ToDouble(flightRoot.time.estimated.arrival));
                }
                catch
                {
                    return new DateTime();
                }
            }
        }
        public DateTime DepartureTime
        {
            get
            {
                try
                {
                    return Util.Helper.GetDateFromEpoch(Convert.ToDouble(flightRoot.time.estimated.departure));
                }
                catch
                {
                    return new DateTime();
                }
            }
        }
        public double DestinationWeather
        {
            get
            {
                try
                {
                    return Util.Helper.kelvinToCelsius(rootWeatherDestination.main.temp);
                }
                catch
                {
                    return -999;
                }
            }
        }
        public double OriginWeather
        {
            get
            {
                try
                {
                    return Util.Helper.kelvinToCelsius(rootWeatherOrigin.main.temp);
                }
                catch
                {
                    return -999;
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
                catch { return ""; }
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
                catch { return ""; }
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
