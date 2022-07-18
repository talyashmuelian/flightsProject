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
                    return new DateTime(0,0,0);
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
                    double lon = flightRoot.airport.destination.position.longitude;
                    double lat = flightRoot.airport.destination.position.latitude;
                    BE.Weather.Root root = bl.GetWeather(lon, lat);
                    return Util.Helper.FahrenheitToCelsius(root.main.temp);
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
                    double lon = flightRoot.airport.origin.position.longitude;
                    double lat = flightRoot.airport.origin.position.latitude;
                    BE.Weather.Root root = bl.GetWeather(lon, lat);
                    return Util.Helper.FahrenheitToCelsius(root.main.temp);
                }
                catch
                {
                    return -999;
                }
            }
        }
        
        public FlightModel(string id)
        {
            flightRoot = bl.GetSelectedFlight(id);
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}
