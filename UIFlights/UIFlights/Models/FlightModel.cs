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
                try
                {
                    return flightRoot.airport.destination.name;
                }
                catch { return "NA"; }
            }
        }

        public string DestinationCountryName
        {
            get
            {
                try
                {
                    return flightRoot.airport.destination.position.country.name;
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string FlightId
        {
            get
            {
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
                    return flightRoot.status.text.Split(' ')[1];
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
                    return "NA";
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
                    return Math.Round(Util.Helper.kelvinToCelsius(rootWeatherDestination.main.temp), 1).ToString();
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
                    return Math.Round(Util.Helper.kelvinToCelsius(rootWeatherOrigin.main.temp), 1).ToString();
                }
                catch
                {
                    return "NA";
                }
            }
        }
        public string DescriptionOrigin
        {
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
        public List<Trail> Trail
        {
            get
            {
                try
                {
                    return flightRoot.trail;
                }
                catch { return new List<Trail>(); }

            }
        }
        public string OriginIATA
        {
            get
            {
                try
                {
                    return flightRoot.airport.origin.code.iata;
                }
                catch { return "NA"; }
            }
        }
        public string DesIATA
        {
            get
            {
                try
                {
                    return flightRoot.airport.destination.code.iata;
                }
                catch { return "NA"; }
            }
        }

        public FlightModel()
        {
        }

        /// <summary>
        /// init asynchronously the fields in this class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FlightModel> initialize(string id)
        {
            flightRoot = await bl.GetSelectedFlightAsync(id);
            try
            {
                double lonOrigin = flightRoot.airport.origin.position.longitude;
                double latOrigin = flightRoot.airport.origin.position.latitude;
                rootWeatherOrigin = await bl.GetWeatherAsync(lonOrigin, latOrigin);
            }
            catch { }
            try
            {
                double lonDes = flightRoot.airport.destination.position.longitude;
                double latDes = flightRoot.airport.destination.position.latitude;
                rootWeatherDestination = await bl.GetWeatherAsync(lonDes, latDes);
            }
            catch { }
            return this;
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}
